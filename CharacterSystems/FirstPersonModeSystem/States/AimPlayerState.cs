using Assets.UnityFoundation.Systems.Character3D.Scripts;
using UnityEngine;
using UnityFoundation.HealthSystem;
using UnityFoundation.ResourceManagement;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class AimPlayerState : BaseCharacterState3D
    {
        private readonly FirstPersonMode player;
        private readonly IResourceManager ammoStorage;

        private bool isFiring = false;

        public AimPlayerState(
            FirstPersonMode player
        )
        {
            this.player = player;
            ammoStorage = player.GetComponent<FiniteResourceManagerMono>();
        }

        public override void EnterState()
        {
            player.AnimController.ToggleAim();
        }

        public override void Update()
        {
            player.Rotate();
            TryReload();
            TryFire();

            if(!isFiring)
                player.Move();
        }

        private void TryReload()
        {
            if(!player.Inputs.Reload) return;

            player.AnimController.Reload();
        }

        private void TryFire()
        {
            if(!player.Inputs.Fire) return;

            isFiring = true;
            player.AnimController.Fire();
        }

        public override void TriggerAnimationEvent(string name)
        {
            if(name == "fire")
                Fire();

            if(name == "finish_shotting")
                isFiring = false;
        }

        private void Fire()
        {
            var ammo = ammoStorage.GetAmount(1);
            if(ammo == 0)
            {
                player.AudioSource.PlayOneShot(player.Settings.FireMissSFX);
                return;
            }

            player.AudioSource.PlayOneShot(player.Settings.FireSFX);
            var ray = new Ray(
                player.WeaponShootPoint.position, player.WeaponShootPoint.forward
            );
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                var damageable = hit.transform.GetComponent<IDamageable>();

                if(damageable != null)
                {
                    damageable.Damage(5f);
                    player.ShootHit(hit.point);
                }
            }
        }
    }
}