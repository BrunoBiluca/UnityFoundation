using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHealthBar {

    public void Setup(float baseHealth);

    // TODO: esse nome pode ser alterado,
    // j� que nem sempre ser� uma quest�o de setar o tamanho da barra
    public void SetSize(float currentHealth);

}
