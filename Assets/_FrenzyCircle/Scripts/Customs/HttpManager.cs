using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace FrenzyCircle
{
    public class HttpManager : MonoBehaviour
    {
        private const string TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" +
                                     ".eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImN" +
                                     "yY2Fzb3VrcnVucHF5dXh0d3piIiwicm9sZSI" +
                                     "6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTcxNjI" +
                                     "2ODMyNywiZXhwIjoyMDMxODQ0MzI3fQ.efA7" +
                                     "_0nxuy7yB5_IRSAqUPw9uefjYuEADU4yCpHyFwY";

        private const string BaseUrl = "https://crcasoukrunpqyuxtwzb.supabase.co/functions/v1";

        public enum RankPeriod
        {
            Daily,
            Weekly,
            Monthly,
        }

        public static IEnumerator IEGetUserScore(UnityAction callback, string id)
        {
            string uri = $"{BaseUrl}/users/{id}/games/1/scores";
            yield return IEGetRequest(uri, GetUserScore_ResponseHandler);
            callback?.Invoke();
        }

        private static void GetUserScore_ResponseHandler(string json)
        {
            ApiResponse<UserRank> response = JsonUtility.FromJson<ApiResponse<UserRank>>(json);
            UserInfo.SetUserRanking(response.data.dayRanking, response.data.weekRanking, response.data.monthRanking);

            Utils.LogFormattedJson("[Get] - UserScore", json);
        }

        public static IEnumerator IEGetAllRanking(UnityAction callback, RankPeriod rankPeriod, int offset = 0,
            int limit = 100)
        {
            string period = rankPeriod switch
            {
                RankPeriod.Daily => "daily",
                RankPeriod.Weekly => "weekly",
                RankPeriod.Monthly => "monthly",
                _ => "daily",
            };

            string uri = $"{BaseUrl}/games/1/rankings?period={period}&offset={offset}&limit={limit}";
            yield return IEGetRequest(uri, GetAllRanking_ResponseHandler);
            callback?.Invoke();
        }

        private static void GetAllRanking_ResponseHandler(string json)
        {
            ApiResponse<List<RankInfo>> response = JsonUtility.FromJson<ApiResponse<List<RankInfo>>>(json);
            UserInfo.SetGlobalRanking(response.data);
            Utils.LogFormattedJson("[Get] - AllRanking", json);
        }

        private static IEnumerator IEGetRequest(string uri, UnityAction<string> callback)
        {
            using UnityWebRequest req = UnityWebRequest.Get(uri);
            req.SetRequestHeader("Authorization", "Bearer " + TOKEN);
            yield return req.SendWebRequest();

            if (req.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Utils.Log($"[GET] CALL Error: {req.error}\nResponse: {req.downloadHandler.text}", true);
            }
            else
            {
                callback?.Invoke(req.downloadHandler.text);
            }
        }
    }
}