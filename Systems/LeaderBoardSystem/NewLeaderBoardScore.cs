namespace Assets.UnityFoundation.Systems.LeaderBoardSystem
{
    public class NewLeaderBoardScore
    {
        public long Score { get; set; }
        public string User { get; set; }

        public static NewLeaderBoardScore Builder()
        {
            return new NewLeaderBoardScore();
        }

        private NewLeaderBoardScore() { }

        public NewLeaderBoardScore WithScore(long score)
        {
            Score = score;
            return this;
        }

        public NewLeaderBoardScore WithUser(string userName)
        {
            User = userName;
            return this;
        }
    }
}