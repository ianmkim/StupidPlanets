using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap};
	public DrawMode drawMode;

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

	public TerrainType[] regions;




	public static Texture2D GenerateColourMap(int mapWidth,
		int mapHeight,
		float noiseScale,
		int octaves,
		float persistance,
		float lacunarity,
		int seed){

		Vector2 offset = new Vector2(0f, 0f);
		TerrainType[] regions = {new TerrainType("Water Deep", 0.3f, new Color(50, 99, 195)),
								new TerrainType("Water Shallow", 0.4f, new Color(54, 103, 199)),
								new TerrainType("Sand", 0.45f, new Color(210, 208, 125)),
								new TerrainType("Grass", 0.55f, new Color(86, 152, 23)),
								new TerrainType("Grass 2", 0.6f, new Color(62, 107, 18)),
								new TerrainType("Rock", 0.7f, new Color(90, 69, 60)),
								new TerrainType("Rock 2", 0.9f, new Color(75, 60, 53)),
								new TerrainType("Snow", 1f, new Color(255, 255, 255))};

		float[,] noiseMap = NoiseMaker.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapWidth + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		return (TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));

	}

	public void GenerateMap() {
		float[,] noiseMap = NoiseMaker.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapWidth + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
		}
	}

	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {

	public TerrainType(string name, float height, Color colour){
		this.name = name;
		this.height = height;
		this.colour = colour;
	}

	public string name;
	public float height;
	public Color colour;
}