[System.Serializable]
public class PlayerData
{
    public int scene;  //номер уровня
    public float[] position;  //координаты

    public PlayerData(GameManager player)  //информация об игроке
    { 
        scene = player.scene;  //номер уровня
        position = new float[2];  //координаты по:
        position[0] = player.savedPosition.x;  // x
        position[1] = player.savedPosition.y;  // y
    }
}
