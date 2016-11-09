using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class visibilityCone:MonoBehaviour  {

    public GameObject sourceOfCone;
    public GameObject[] CollisionPoints;
    public GameObject fogOfWarPlane;
    public static List<Vector2> pixelsColored;
    [Range(1,4)]
    public int sampleModifier=2;
    public int Radius = 50;

    public Color ColorOfCone;
    public Color ColorOfPath;
    public Color ColorOfFog;
    private int dimensionsOfQuad = 64, dimensionsOfTexture = 512,n;
    private float auxrad = 0;
    private struct vertexData
    {
        private Vector3 position;
        private Vector2 iv;
        public float length;

        public Vector3 Position
        {
            get
            {
                return position;
            }


        }

        public Vector2 Iv
        {
            get
            {
                return iv;
            }

            set
            {
                iv = value;
            }
        }

        public vertexData(Vector3 position,Vector2 iv)
        {
            this.position = position;
            this.iv = iv;
            this.length = position.magnitude;
        }

        public static vertexData operator /(vertexData data, float alpha)
        {
            return new vertexData(data.Position/alpha,data.Iv/alpha);
        }

        public static vertexData operator *(vertexData data, float alpha)
        {
            return new vertexData(data.Position * alpha, data.Iv * alpha);
        }

        public static vertexData operator -(vertexData data1, vertexData data2)
        {
            return new vertexData(data1.Position-data2.Position, data1.Iv -data2.Iv);
        }

        public float getPositionLength()
        {
            return this.Position.magnitude;
        }

        public string toString()
        {
            return " {" + position + "," + iv + "}";
        }
    }
    private Texture2D fogOfWarText;



	// Use this for initialization
	void Start () {
      
        fogOfWarText =(Texture2D)fogOfWarPlane.GetComponent<Renderer>().material.mainTexture;
        pixelsColored = new List<Vector2>();
        for (int y = 0; y < fogOfWarText.height; y++)
        {
            for (int x = 0; x < fogOfWarText.width; x++)
            {
                fogOfWarText.SetPixel(x, y, ColorOfFog);
            }
        }
        dimensionsOfTexture = fogOfWarText.width;
        dimensionsOfQuad = (int)fogOfWarPlane.transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {
        Vector3 sourcePos = sourceOfCone.transform.position;

        auxrad = Radius * sampleModifier;
        vertexData a = new vertexData(sourcePos, getIvsat(sourcePos));
        for (int i = 0; i < pixelsColored.Count; i++)
        {
            fogOfWarText.SetPixel((int)(pixelsColored[i].x), (int)(pixelsColored[i].y), ColorOfPath);
        }

        pixelsColored.Clear();

        for (int i = 0; i < CollisionPoints.Length - 1; i++)
        {
            Vector3 tempb = CollisionPoints[i].transform.position, tempc = CollisionPoints[i + 1].transform.position;

            float distab = Vector2.Distance(sourcePos, tempb);
            float distac = Vector2.Distance(sourcePos, tempc);

            vertexData[] data = getBCData(tempb, tempc);
            vertexData b = data[0];
            vertexData c = data[1];

            float tempDist = (distab > distac ? distab : distac);
            float n = tempDist / dimensionsOfQuad * dimensionsOfTexture * sampleModifier;

            // Debug.Log("itero " + n + "veces");
            iterateOverCone((int)n, ref a, ref b, ref c);
        }

        fogOfWarText.Apply();

    }

    private void iterateOverCone(int n ,ref vertexData a,ref vertexData b,ref  vertexData c)
    {
        float alpha;
        Color auxColor = ColorOfCone;
        for (int i = 0; i < n; i++)
        {
            float scaler_i = (float)i / n;
            alpha = i / (float)auxrad;
            vertexData cprime = c * scaler_i;
            cprime.Iv = Vector2.Lerp(a.Iv, c.Iv, scaler_i);
            vertexData bprime = b * scaler_i;
            bprime.Iv = Vector2.Lerp(a.Iv, b.Iv, scaler_i);

            
            vertexData bprime_cprime = cprime - bprime;
            float length = bprime_cprime.length / dimensionsOfQuad * dimensionsOfTexture;


            for (int j = 0; j <length; j++)
            {
                float scaler_j = j / length;
                Vector2 pixelOfTexture =Vector2.Lerp(bprime.Iv, cprime.Iv, scaler_j) *dimensionsOfTexture;
                pixelsColored.Add(pixelOfTexture);
                auxColor.a = 1-alpha;
                fogOfWarText.SetPixel((int)(pixelOfTexture.x),(int)(pixelOfTexture.y), (ColorOfCone*(1-alpha)+ColorOfPath*alpha));
            }
        }
    }

    private Vector2 getIvsat(Vector3 position)
    {
        RaycastHit ray;

        if (Physics.Raycast(position, Vector3.forward,out ray))
        {
          
            if(ray.collider.tag == "FogOfWar")
            {
                return ray.textureCoord;
            }
        }
        Debug.LogError("NO COLLISION WITH FOGOFWAR");
        return new Vector2();
    }

    private vertexData[] getBCData(Vector3 b, Vector3 c)
    {
        vertexData bdata = new vertexData(b, getIvsat(b));
        vertexData cdata = new vertexData(c, getIvsat(c));

        vertexData[] data = { bdata,cdata};
        return data;

    }
    public void addCollisionPoints(GameObject[] CollisionPoints) {
        this.CollisionPoints = CollisionPoints;
    }
}
