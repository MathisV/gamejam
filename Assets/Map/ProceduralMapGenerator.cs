using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralMapGenerator : MonoBehaviour
{
    public Tilemap tilemap; // Référence à la Tilemap pour générer la carte

    public TileBase groundTile; // Tile pour le sol

    public TileBase mountainTile; // Tile pour les montagnes

    public TileBase interiorTile; // Tile pour l'intérieur des montagnes

    public int mapWidth = 100; // Largeur de la carte en tiles

    public int mapHeight = 20; // Hauteur de la carte en tiles

    public float mountainHeight = 5f; // Hauteur de la montagne

    public float mountainFrequency = 0.2f; // Fréquence des pics de montagne

    public float holeFrequency = 0.2f; // Fréquence des trous

    public int holeSizeMin = 3; // Taille minimale des trous

    public int holeSizeMax = 6; // Taille maximale des trous

    public Transform player; // Référence au joueur pour générer la carte en fonction de sa position

    private int lastPlayerX = 0; // Dernière position en X du joueur

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        // Vérifie si le joueur a avancé d'une tile en X pour générer la suite de la carte
        int currentPlayerX = Mathf.FloorToInt(player.position.x);
        if (currentPlayerX > lastPlayerX)
        {
            lastPlayerX = currentPlayerX;
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        // Calcule la position de départ pour générer la nouvelle partie de la carte
        Vector3Int startCell = new Vector3Int(lastPlayerX + mapWidth, 0, 0);
        Vector3Int endCell = startCell + new Vector3Int(0, mapHeight, 0);

        // Remplit la zone avec le sol
        tilemap.BoxFill(startCell, groundTile, 0, 0, mapWidth, mapHeight);

        // Génère les montagnes de manière aléatoire
        for (int x = 0; x < mapWidth; x++)
        {
            float mountainHeightOffset =
                Mathf.PerlinNoise((startCell.x + x) * mountainFrequency, 0) *
                mountainHeight;
            int mountainHeightInt = Mathf.FloorToInt(mountainHeightOffset);
            for (int y = 0; y < mountainHeightInt; y++)
            {
                // Si on est à la hauteur maximale de la montagne, on place la tile de terre
                if (y == mountainHeightInt - 1)
                {
                    tilemap
                        .SetTile(new Vector3Int(startCell.x + x,
                            startCell.y + y,
                            0),
                        groundTile);
                }
                else
                // Sinon, on place la tile de pierre
                {
                    tilemap
                        .SetTile(new Vector3Int(startCell.x + x,
                            startCell.y + y,
                            0),
                        mountainTile);
                }
            }

            // Remplit l'intérieur des montagnes avec la tile de pierre
            /*for (int y = mountainHeightInt; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(startCell.x + x, startCell.y + y, 0), mountainTile);
            }*/
            // Génère les trous de manière aléatoire
            if (Random.value < holeFrequency)
            {
                int holeSize = Random.Range(holeSizeMin, holeSizeMax + 1);
                int holeCenter = mountainHeightInt + holeSize / 2;
                for (int y = 0; y < holeSize; y++)
                {
                    int holePosition = holeCenter - holeSize / 2 + y;
                    tilemap
                        .SetTile(new Vector3Int(startCell.x + x,
                            startCell.y + holePosition,
                            0),
                        null);
                }
            }
        }
    }
}
