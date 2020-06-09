
[System.Serializable]
public class PlayerSaveData
{
    public int Score { get; set; }
    public int Money { get; set; }
    public float Health { get; set; }

    public float[] position;

    public PlayerSaveData(PlayerInformation player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        this.Health = player.Health;
        this.Score = player.Score;
        this.Money = player.Money;
    }
}
