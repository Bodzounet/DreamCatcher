using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapController : MonoBehaviour {

    [SerializeField]
    private string _mapName;
	private System.Xml.XmlDocument map;
	private int height, width, tileHeight, tileWidth, tilesetWidth, tilesetHeight;
	private List<GameObject> tileset;
	private System.Xml.XmlNodeList _gidMap;
    private List<Texture2D> tilesetTextures;
    private int center;
    public PhysicsMaterial2D pmaterial;
	private int _i = 0;
	// Use this for initialization
	void Start () 
	{
        System.DateTime beg = System.DateTime.Now;
		map = new System.Xml.XmlDocument();
        map.Load(File.OpenText("Smap/" + _mapName));

		System.Xml.XmlNode node;
        #region Map Properties

		node = map.GetElementsByTagName("map").Item(0);

        center = int.Parse(map.GetElementsByTagName("properties").Item(0).ChildNodes.Item(0).Attributes["value"].Value);
		width = int.Parse(node.Attributes["width"].Value);
		height = int.Parse(node.Attributes["height"].Value);
		tileWidth = int.Parse(node.Attributes["tilewidth"].Value);
		tileHeight = int.Parse(node.Attributes["tileheight"].Value);
        #endregion


        #region Tileset
        tilesetTextures = new List<Texture2D>();
        tileset = new List<GameObject>();
        for (int t = 0; map.GetElementsByTagName("tileset").Item(t) != null; t++)
        {

            node = map.GetElementsByTagName("tileset").Item(t);
            if (node.Attributes["source"] != null)
            {
                System.Xml.XmlDocument tsx = new System.Xml.XmlDocument();
                tsx.Load(File.OpenText("Smap/" + node.Attributes["source"].Value));
                node = tsx.GetElementsByTagName("tileset").Item(t);
            }
            node = node.FirstChild;

            //Pensez à lire les .tsx

            byte[] data = File.ReadAllBytes("Smap/" + node.Attributes["source"].Value);

            tilesetWidth = int.Parse(node.Attributes["width"].Value);
            tilesetHeight = int.Parse(node.Attributes["height"].Value);

            tilesetTextures.Add(new Texture2D(tilesetWidth, tilesetHeight, TextureFormat.RGB24, false));
            tilesetTextures[t].LoadImage(data);
            tilesetTextures[t].filterMode = FilterMode.Point;
            tilesetTextures[t].wrapMode = TextureWrapMode.Clamp;
            tilesetTextures[t].mipMapBias = -0.5f;
            while (tileset.Count < int.Parse(map.GetElementsByTagName("tileset").Item(t).Attributes["firstgid"].Value) - 1)
                tileset.Add(null);
            for (int y = tilesetHeight; y > 0; y -= tileHeight)
            {
                for (int x = 0; x < tilesetWidth; x += tileWidth)
                {
                    GameObject tmp = new GameObject();

                    tmp.AddComponent("SpriteRenderer");

                    tmp.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tilesetTextures[t], new Rect(x, y - tileHeight, tileWidth, tileHeight), new Vector2(0.5f, 0.5f), 50.0f);
                    /* tmp.AddComponent<MeshFilter>().mesh = GameObject.Find("Player").GetComponent<MeshFilter>().mesh;
                     Texture2D textureTmp = new Texture2D(tilesetWidth / tileWidth, tilesetHeight / tileHeight, TextureFormat.RGBA32, false);
                     textureTmp.SetPixels(0, 0, tilesetWidth / tileWidth, tilesetHeight / tileHeight, tilesetTextures.GetPixels(x,y - tileHeight,tileWidth, tileHeight));
                     Material mat = new Material(Shader.Find("Transparent/Diffuse"));
                     mat.SetTexture(0, textureTmp);
                     tmp.AddComponent<MeshRenderer>().material = mat;*/

                    tmp.isStatic = true;
                    tmp.SetActive(false);
                    tileset.Add(tmp);
                }
            }

        }
        #endregion

        #region Construction de la map
        int i = 0;
		//@todo Faire un foreach pour les layers

        for (int l = 0; l < map.GetElementsByTagName("data").Count; l++)
        {
            node = map.GetElementsByTagName("data").Item(l);
            _gidMap = node.ChildNodes;
            _i = 0;
            int max = node.ChildNodes.Count;
            while (_i < max)
            {


                if (_gidMap[_i].Attributes["gid"].Value != "0")
                {
                    GameObject tmp;
                    tmp = Instantiate(tileset[int.Parse(_gidMap[_i].Attributes["gid"].Value) - 1]) as GameObject;
                    tmp.transform.position = new Vector3((((_i) % width) * tileWidth) / 50.0f, (((_gidMap.Count - 1) - _i) / width * tileHeight) / 50.0f, 0);
                    tmp.name = "Block_" + _i;
                    tmp.transform.parent = this.transform;
                    tmp.GetComponent<SpriteRenderer>().sortingOrder = l - map.GetElementsByTagName("data").Count + 1;
                    if (l == map.GetElementsByTagName("data").Count - 1)
                    {
                        tmp.AddComponent<PolygonCollider2D>();
                        tmp.layer = LayerMask.NameToLayer("ground");
                        if (_i % width > center)
                        {

                            Vector2[] points = tmp.GetComponent<PolygonCollider2D>().points;
                            for (int p = 0; p < points.Length; p++)
                            {
                                points[p] = new Vector2(points[p].x + (tileWidth * (center - _i % width)) * 2 / 50.0f, points[p].y);
                            }
                            tmp.GetComponent<PolygonCollider2D>().points = points;
                        }
                        else if (_i % width == center)
                        {
                            tmp.layer = LayerMask.NameToLayer("center");
                        }
                    }
                    tmp.SetActive(true);
                }
                _i++;
            }
        }
		if (_i < 0)
			Destroy(this);
        #endregion

        #region Couche Object
		node = map.GetElementsByTagName("objectgroup").Item(0);
		if (node != null)
		{
			for (i= 0; i < node.ChildNodes.Count ; i++)
			{  
				if(node.ChildNodes[i].Attributes["type"].Value == "spawn")
				{
					GameObject.Find("P1").transform.position = new Vector3(int.Parse(node.ChildNodes[i].Attributes["x"].Value) / 50.0f, ((height * tileHeight) / 50.0f) - int.Parse(node.ChildNodes[i].Attributes["y"].Value) / 50.0f, 0);
				}
				else
				{
					GameObject newEnt = Instantiate(Resources.Load(node.ChildNodes[i].Attributes["type"].Value, typeof(GameObject) )) as GameObject;
					newEnt.transform.position = new Vector3(int.Parse(node.ChildNodes[i].Attributes["x"].Value) / 50.0f, ((height * tileHeight) / 50.0f) - int.Parse(node.ChildNodes[i].Attributes["y"].Value) / 50.0f, 0);
                    newEnt.transform.position -= new Vector3(0, (int.Parse(node.ChildNodes[i].Attributes["height"].Value) / tileHeight) / 2 * tileHeight / 50.0f + tileHeight / 100.0f, 0);
                    newEnt.transform.localScale = new Vector3(int.Parse(node.ChildNodes[i].Attributes["width"].Value) / tileWidth, (int.Parse(node.ChildNodes[i].Attributes["height"].Value) / tileHeight));
					//entities.Add(node.ChildNodes[i].Attributes["name"].Value, newEnt);
				}

			}
        }
        Debug.Log("generé en : " + (System.DateTime.Now - beg).TotalSeconds + " seconds");
        Camera.main.transform.position = new Vector3((width / 2 * tileWidth) / 50.0f, (height / 2 * tileHeight) / 50.0f, Camera.main.transform.position.z);
        #endregion
    }
	
	// Update is called once per frame
    void Update () {
    //    int objectif = _i - 10;
    //    while(_i >= 0 && _i > objectif)
    //    {   
    //        if(_gidMap[_i].Attributes["gid"].Value != "0")
    //        {
    //            GameObject tmp;
    //            tmp = Instantiate(tileset[int.Parse(_gidMap[_i].Attributes["gid"].Value) - 1]) as GameObject;
    //            tmp.transform.position = new Vector3(((( _i) % width) * tileWidth) / 50.0f, (((_gidMap.Count - 1) - _i) / width  * tileHeight) / 50.0f, 0);
            
    //            tmp.name = "Block_" + _i;
    //        }
    //        _i--;
    //    }
    //    if (_i < 0)
    //        Destroy(this);
	}

	void OnGUI() {

	}
}
