
[System.Serializable]
public class ApiResponse<T>
{
    public int resultCode;
    public string message;
    public T data;
}

[System.Serializable]
public class PlayerInfo
{
    public int id;
    public string nickname;
    public int todayHighestScore;
    public int todayRank;
}
