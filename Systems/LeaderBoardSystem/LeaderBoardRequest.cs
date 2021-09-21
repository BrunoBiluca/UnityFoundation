using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.Code.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.LeaderBoardSystem
{
    public class LeaderBoardRequest : Singleton<LeaderBoardRequest>
    {
        private const string Protocol = "https";
        private const string Host = "localhost";
        private const string Port = "44376";
        private const string LeaderboardEndpoint = "api/leaderboards";
        private const string UserLeaderboardEndpoint = "api/users/<user>/leaderboards";

        public string URL => $"{Protocol}://{Host}:{Port}/{LeaderboardEndpoint}";
        public string USER_URL => $"{Protocol}://{Host}:{Port}/{UserLeaderboardEndpoint}";

        public void AddScore(
            NewLeaderBoardScore newScore,
            Action<LeaderBoardScoreModel> onSuccess,
            Action<string> onError
        )
        {
            WebRequests.Post(
                URL,
                JsonConvert.SerializeObject(
                    newScore,
                    new JsonSerializerSettings() {
                        ContractResolver = new DefaultContractResolver {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    }
                ),
                (response) => {
                    var leaderBoardScore = JsonConvert
                        .DeserializeObject<LeaderBoardScoreModel>(response);
                    onSuccess(leaderBoardScore);
                },
                (error) => {
                    Debug.Log(error);
                    onError(error);
                }
            );
        }

        public void GetHighestScores(
            int limit,
            Action<List<LeaderBoardScoreModel>> onSuccess,
            Action<string> onError
        )
        {
            WebRequests.Get(
                $"{URL}?order=desc&limit={limit}",
                (response) => {
                    var highestScores = JsonConvert
                        .DeserializeObject<List<LeaderBoardScoreModel>>(response);
                    onSuccess(highestScores);
                },
                (error) => {
                    Debug.Log(error);
                    onError(error);
                }
            );
        }

        public void GetUserPersonalRecord(
            string user,
            Action<LeaderBoardScoreModel> onSuccess,
            Action<string> onError
        )
        {
            string url = USER_URL.Replace("<user>", user);
            WebRequests.Get(
                $"{url}?order=desc",
                (response) => {
                    var personalRecord = JsonConvert
                        .DeserializeObject<List<LeaderBoardScoreModel>>(response);

                    if(personalRecord.Count == 0)
                    {
                        onSuccess(null);
                        return;
                    }

                    onSuccess(personalRecord[0]);
                },
                (error) => {
                    Debug.Log(error);
                    onError(error);
                }
            );
        }
    }
}