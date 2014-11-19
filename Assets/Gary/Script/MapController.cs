using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapController : MonoBehaviour {

    [SerializeField]
    private string _mapName;
	private System.Xml.XmlDocument map;
	public int height, width, tileHeight, tileWidth, tilesetWidth, tilesetHeight;
	private List<GameObject> tileset;
	private System.Xml.XmlNodeList _gidMap;
    private List<Texture2D> tilesetTextures;
    public int center;
    public PhysicsMaterial2D pmaterial;
    public float pixelToUnit = 100;
	private int _i = 0;
    public GameObject hidden;
    public bool centerH = false;
    public bool print = true;
    public GameObject lightPoint;
    public Sprite hideBlock;
	// Use this for initialization
	void Start () 
	{
        if (hidden == null)
            hidden = GameObject.Find("HiddenEntities");

        System.DateTime beg = System.DateTime.Now;
		map = new System.Xml.XmlDocument();
        map.Load(File.OpenText("Smap/" + _mapName));

		System.Xml.XmlNode node;
        #region Map Properties

		node = map.GetElementsByTagName("map").Item(0);

        System.Xml.XmlNode prop = map.GetElementsByTagName("properties").Item(0);
        for (int n = 0; n < prop.ChildNodes.Count; n++)
        {
            if (prop.ChildNodes[n].Attributes["name"].Value == "center")
                center = int.Parse(prop.ChildNodes[n].Attributes["value"].Value);
            if (prop.ChildNodes[n].Attributes["name"].Value == "centerH")
                centerH = true;
        }
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

                    tmp.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tilesetTextures[t], new Rect(x, y - tileHeight, tileWidth, tileHeight), new Vector2(0.5f, 0.5f), pixelToUnit);
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
            node = map.GetElementsByTagName("layer").Item(l);
            bool hide = false;
            if (node.Attributes["name"].Value == "Hidden")
                hide = true;
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
                    tmp.transform.position = new Vector3((((_i) % width) * tileWidth) / pixelToUnit, (((_gidMap.Count - 1) - _i) / width * tileHeight) / pixelToUnit, 0);
                    tmp.name = "Block_" + _i;


                    tmp.GetComponent<SpriteRenderer>().sortingOrder = l - map.GetElementsByTagName("data").Count + 1;
                    if (!print && !hide)
                        tmp.GetComponent<SpriteRenderer>().enabled = false;
                    if (l == map.GetElementsByTagName("data").Count - 1 || hide)
                    {
                        tmp.AddComponent<PolygonCollider2D>();
                        tmp.layer = LayerMask.NameToLayer("ground");

                            Vector2[] points = tmp.GetComponent<PolygonCollider2D>().points;
                            float yDown = points[0].y;
                            for (int p = 0; p < points.Length; p++)
                            {
                                if (!centerH && _i % width > center)
                                {
                                    points[p] = new Vector2(points[p].x + (tileWidth * (center - _i % width)) * 2 / pixelToUnit, points[p].y);
                                }
                                else if (centerH && ((_gidMap.Count - 1) - _i) / width < center)
                                {
                                    points[p] = new Vector2(points[p].x + (tileWidth * ((width / 2) - _i % width)) * 2 / pixelToUnit, points[p].y + ((center + 1) * tileHeight / pixelToUnit));
                                }

                                if (points[p].y <= yDown)
                                {
                                    points[p] = new Vector2(points[p].x, points[p].y - 0.01f);
                                }


                            }
                            tmp.GetComponent<PolygonCollider2D>().points = points;
                        
                     if (!centerH && _i % width == center)
                        {
                            tmp.layer = LayerMask.NameToLayer("center");
                          }
                     else if (centerH && ((_gidMap.Count - 1) - _i) == center)
                         {
                             tmp.layer = LayerMask.NameToLayer("center");
                         }
                    }
                    if (hide)
                    {
                        tmp.transform.parent = hidden.transform;
                        GameObject newLight = Instantiate(lightPoint) as GameObject;
                        newLight.transform.position = tmp.transform.position;
                        newLight.transform.parent = GameObject.Find("ShownEntities").transform;
                        tmp.GetComponent<SpriteRenderer>().sprite = hideBlock;
                        tmp.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }
                    else
                        tmp.transform.parent = this.transform;
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
        Dictionary<string, GameObject> torches = new Dictionary<string, GameObject>();
		if (node != null)
		{
			for (i= 0; i < node.ChildNodes.Count ; i++)
			{  
				if(node.ChildNodes[i].Attributes["type"].Value == "spawn")
				{
					GameObject.Find("CharacterLeft").transform.position = new Vector3(int.Parse(node.ChildNodes[i].Attributes["x"].Value) / pixelToUnit, ((height * tileHeight) / pixelToUnit) - int.Parse(node.ChildNodes[i].Attributes["y"].Value) / pixelToUnit, 0);
                    GameObject.Find("CharacterLeft").GetComponent<MovementController>().spawnPos = GameObject.Find("CharacterLeft").transform.position;
				}
				else
				{
					GameObject newEnt = Instantiate(Resources.Load(node.ChildNodes[i].Attributes["type"].Value, typeof(GameObject) )) as GameObject;
					newEnt.transform.position = new Vector3(int.Parse(node.ChildNodes[i].Attributes["x"].Value) / pixelToUnit, ((height * tileHeight) / pixelToUnit) - int.Parse(node.ChildNodes[i].Attributes["y"].Value) / pixelToUnit, 0);
                    GameObject copie = null;

                    if (!node.ChildNodes[i].Attributes["name"].Value.Contains("Key") && !node.ChildNodes[i].Attributes["name"].Value.Contains("Locked") && && !centerH && int.Parse(node.ChildNodes[i].Attributes["x"].Value) / tileWidth > center)
                    {
                        copie = Instantiate(newEnt) as GameObject;
                        copie.name = node.ChildNodes[i].Attributes["name"].Value; 
                        copie.transform.position = new Vector3(((center * tileWidth) - (int.Parse(node.ChildNodes[i].Attributes["x"].Value) - (center * tileWidth))) / pixelToUnit, newEnt.transform.position.y, 0);
                        copie.transform.position -= new Vector3(0, ((int.Parse(node.ChildNodes[i].Attributes["height"].Value) / 2) * 1.25f) / pixelToUnit, 0);
                        if (copie.renderer != null)
                            copie.renderer.enabled = false;
                        else if (copie.transform.GetChild(0).renderer != null)
                            copie.transform.GetChild(0).renderer.enabled = false;
                    }
                    else if (!node.ChildNodes[i].Attributes["name"].Value.Contains("Key") && !node.ChildNodes[i].Attributes["name"].Value.Contains("Locked") && centerH && int.Parse(node.ChildNodes[i].Attributes["y"].Value) / tileWidth > center)
                    {
                        copie = Instantiate(newEnt) as GameObject;
                        copie.name = node.ChildNodes[i].Attributes["name"].Value;
                        copie.transform.position = new Vector3(((width / 2 * tileWidth) - (int.Parse(node.ChildNodes[i].Attributes["x"].Value) - (width / 2 * tileWidth))) / pixelToUnit, newEnt.transform.position.y + (center * tileHeight / pixelToUnit), 0);
                        copie.transform.position -= new Vector3(0, ((int.Parse(node.ChildNodes[i].Attributes["height"].Value) / 2) * 0.8f) / pixelToUnit, 0);
                        if (copie.renderer != null)
                            copie.renderer.enabled = false;
                        else if (copie.transform.GetChild(0).renderer != null)
                            copie.transform.GetChild(0).renderer.enabled = false;
                    }
                    else
                        newEnt.name = node.ChildNodes[i].Attributes["name"].Value; 
                    newEnt.transform.position -= new Vector3(0, ((int.Parse(node.ChildNodes[i].Attributes["height"].Value) / 2) * 1.25f) / pixelToUnit, 0);
                    if (node.ChildNodes[i].ChildNodes[0] != null && node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["name"].Value == "scale")
                    {
                        newEnt.GetComponent<Ladder>().setHeight(int.Parse(node.ChildNodes[i].Attributes["height"].Value) / 32);
                        /*newEnt.transform.localScale = new Vector3(int.Parse(node.ChildNodes[i].Attributes["width"].Value) / tileWidth, (int.Parse(node.ChildNodes[i].Attributes["height"].Value) / tileHeight));
                        newEnt.transform.position += Vector3.right * (int.Parse(node.ChildNodes[i].Attributes["width"].Value)  / 4 / pixelToUnit);
                        if (copie != null)
                        {
                            copie.transform.localScale = new Vector3(int.Parse(node.ChildNodes[i].Attributes["width"].Value) / tileWidth, (int.Parse(node.ChildNodes[i].Attributes["height"].Value) / tileHeight));
                            copie.transform.position += Vector3.left * (int.Parse(node.ChildNodes[i].Attributes["width"].Value) / 4 / pixelToUnit);
                            newEnt.transform.localScale = new Vector3(1, 1, 1);
                        }*/
                    }
                    if (copie != null)
                    {
                        newEnt.transform.parent = copie.transform;
                        newEnt.transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (node.ChildNodes[i].ChildNodes[0] != null && node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["name"].Value == "link")
                    {
                        if (copie != null)
                        {
                            newEnt.transform.parent = null;
                            Destroy(copie);
                        }
                        torches[node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["value"].Value] = newEnt;


                        if (node.ChildNodes[i].ChildNodes[0].ChildNodes[1].Attributes["name"].Value == "on")
                        {
                            if (node.ChildNodes[i].ChildNodes[0].ChildNodes[1].Attributes["value"].Value == "true")
                                newEnt.GetComponent<switchTorchMode>().Invert();
                        }
                    }
                    if (node.ChildNodes[i].ChildNodes[0] != null && node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["name"].Value == "id")
                    {
                        GameObject doorId = GameObject.Find("Door" + node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["value"].Value);
                        if (doorId == null)
                        {
                            doorId = new GameObject();
                            doorId.transform.position = Vector3.zero;
                            doorId.name = "Door" + node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["value"].Value;
                            if (copie != null)
                            {
                                copie.transform.parent = doorId.transform;
                                copie.GetComponent<Door>().id = int.Parse(node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["value"].Value);
                            }
                            else
                            {
                                newEnt.transform.parent = doorId.transform;
                                newEnt.GetComponent<Door>().id = int.Parse(node.ChildNodes[i].ChildNodes[0].ChildNodes[0].Attributes["value"].Value);
                            }
                        }
                        else
                        {
                           
                            if (copie != null)
                            {
                                copie.GetComponent<Door>().other = doorId.transform.GetChild(0);
                                doorId.transform.GetChild(0).GetComponent<Door>().other = copie.transform;
                            }
                            else
                            {
                                newEnt.GetComponent<Door>().other = doorId.transform.GetChild(0);
                                doorId.transform.GetChild(0).GetComponent<Door>().other = newEnt.transform;
                            }
                            doorId.transform.GetChild(0).parent = null;
                            Destroy(doorId);
                        }
                    }
					//entities.Add(node.ChildNodes[i].Attributes["name"].Value, newEnt);
				}

			}
            foreach (KeyValuePair<string, GameObject> torc in torches)
            {
                torc.Value.GetComponent<switchTorchMode>().link = GameObject.Find(torc.Key);
            }
        }
        Debug.Log("generé en : " + (System.DateTime.Now - beg).TotalSeconds + " seconds");
        Camera.main.transform.position = new Vector3((width / 2 * tileWidth) / pixelToUnit, (height / 2 * tileHeight) / pixelToUnit, Camera.main.transform.position.z);
        while (tileset.Count > 0)
        {
            Destroy(tileset[0]);
            tileset.Remove(tileset[0]);
        }
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
    //            tmp.transform.position = new Vector3(((( _i) % width) * tileWidth) / pixelToUnit, (((_gidMap.Count - 1) - _i) / width  * tileHeight) / pixelToUnit, 0);
            
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
