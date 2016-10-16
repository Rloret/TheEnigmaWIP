using UnityEngine;
using System.Collections;
using System;

public class TileMap : MonoBehaviour {

    private static TileMap _instance;

    public static TileMap getTileMapInstance { get { return _instance; } }


    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D; // para almacenar la textura que vamos a usar para sacar las tiles
    public Vector2 tileSize = new Vector2();
    public Vector2 tilePadding = new Vector2();
    public UnityEngine.Object[] spriteReferences;
    public Vector2 gridSize = new Vector2();
    public int pixelsToUnits = 100;
    public int tileID = 0;
    public GameObject tiles;
    public tileData[,] tilesDataMap;

    public Sprite currentTileBrush {
        get { return spriteReferences[tileID] as Sprite; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        tilesDataMap = new tileData[(int)mapSize.x, (int)mapSize.y];
        foreach (Transform child in tiles.transform)
        {
            int[] values = parseString(child.gameObject.name);
            tilesDataMap[values[0], values[1]] = new tileData(new Vector2(child.position.x, child.position.y), values[2], child.gameObject);
           // Debug.Log(tilesDataMap[values[0], values[1]].Type);
        }

    }

    private int[] parseString(string s)
    {
        int counter = 0;
        int[] values = { 0, 0, 0};
        string valueAux = "";
        //  Debug.Log(s);
        for (int i = 0; i < s.Length; i++)

        {
            if (s[i] == '#')
            {
                values[counter] = Convert.ToInt32(valueAux);
                counter++;
                // Debug.Log("valor numero " + counter + "=" + valueAux);
                valueAux = "";

            }
            else
            {
                valueAux += s[i].ToString();
            }
        }

        return values;
    }

   /* private Vector3 parseString(string s)
    {
        int counter = 0; ;
        string auxValueString = "";
        Vector3 data = new Vector3();
        foreach (var c in s)
        {
            if (c != '#')
            {
                auxValueString += c;
                Debug.Log(auxValueString);
            }
            else
            {
                counter++;
                switch (counter)
                {
                    case 1:
                        data.x =Convert.ToInt32(auxValueString);
                        break;
                    case 2:
                        data.y = Convert.ToInt32(auxValueString);
                        break;
                    case 3:
                        data.z = Convert.ToInt32(auxValueString);
                        break;
                    default:
                        break;
                }
            }
            auxValueString = "";
        }
        return data;
    }*/
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(tileID);
    }

    void OnDrawGizmosSelected() {
        var pos = transform.position;

        if (texture2D != null) {

            //Esto es para dibujar la malla
            Gizmos.color = Color.gray;
            var row = 0;
            var maxColumns = mapSize.x;
            var total = mapSize.x * mapSize.y;
            var tile = new Vector3(tileSize.x / pixelsToUnits, tileSize.y / pixelsToUnits);
            var offset = new Vector2(tile.x / 2 , tile.y / 2);

            for (var i = 0; i < total; i++){

                var column = i % maxColumns;

                var newX = (column * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y;

                Gizmos.DrawWireCube(new Vector2(newX, newY), tile);

                if (column == maxColumns - 1) {
                    row++;
                }
            }

            //Esto es para dibujar el borde
            Gizmos.color = Color.white;
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y - (gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize);
        }

    }

    /*public void restartTilesDataMap()
    {
        tilesDataMap = new tileData[(int)mapSize.x, (int)mapSize.y];
    }

    public void addTileDataToTilesDataMap(int id,GameObject tile) {
        int fila = (int)(id/mapSize.x);
        int columna=(int)(id%mapSize.x);

        //Debug.Log(fila + "," + columna + ","+ tileID);

        tilesDataMap[columna, fila] = new tileData(new Vector2(tile.transform.position.x, tile.transform.position.y), tileID,tile);
        Debug.Log(tilesDataMap[columna, fila].stringValuesForDebug());
    }
    public void removeTileDataToTilesDataMap(int id)
    {
        int fila = (int)(id / mapSize.x);
        int columna = (int)(id % mapSize.x) - 1;

        tilesDataMap[columna, fila] = new tileData();

    }*/

    public struct tileData
    {
        public Vector2 Centre;
        private int type;
        public GameObject Holder;

        public int Type
        {
            get
            {
                return type;
            }

        }

        public tileData(Vector2 tileCentre, int tileType,GameObject gameObject) {
            Centre = tileCentre;
            type = tileType;
            Holder = gameObject;
        }

        public string stringValuesForDebug()
        {
            return "centre is" + Centre.ToString() + " type is " + type + "Holder name is " + Holder.name;
        }

    }
}



