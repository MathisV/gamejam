using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralMapGenerator : MonoBehaviour
{
    public Tilemap tilemap; // Référence à la Tilemap pour générer la carte

    public TileBase groundTile; // Tile pour le sol

    public TileBase mountainTile; // Tile pour les montagnes

    public TileBase rigthMountainTile; // Tile pour la droite des montagnes

    public TileBase leftMountainTile; // Tile pour la gauche des montagnes

    public TileBase interiorTile; // Tile pour l'intérieur des montagnes

    public TileBase rightInteriorTile; // Tile pour l'intérieur droite des montagnes

    public TileBase leftInteriorTile; // Tile pour l'intérieur gauche des montagnes

    public int mapWidth = 50; // Largeur de la carte en tiles

    public int mapHeight = 20; // Hauteur de la carte en tiles

    public float mountainHeight = 5f; // Hauteur de la montagne

    public float mountainFrequency = 0.2f; // Fréquence des pics de montagne

    public float holeFrequency = 0.2f; // Fréquence des trous

    public int holeSizeMin = 3; // Taille minimale des trous

    public int holeSizeMax = 6; // Taille maximale des trous

    public Transform player; // Référence au joueur pour générer la carte en fonction de sa position

    public GameObject switchPrefab; // Référence au prefab du switch

    public GameObject bridgePrefab; // Référence au prefab du pont

    private GameObject lastSwitch; // Référence au dernier switch généré

    private int lastPlayerX = 0; // Dernière position en X du joueur

    void Start()
    {
        GenerateMap(first: true);
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

    void GenerateMap(bool first = false)
    {
        // Calcule la position de départ pour générer la nouvelle partie de la carte
        Vector3Int startCell = new Vector3Int(lastPlayerX + mapWidth, 0, 0);
        Vector3Int endCell = startCell + new Vector3Int(1, mapHeight, 0);

        // Remplit la zone avec le sol
        tilemap.BoxFill(startCell, groundTile, 0, 0, mapWidth, mapHeight);

        int pastHeight = 0;
        int nextHeight = getMountainHeight(startCell, 0);

        // Génère les montagnes de manière aléatoire
        for (int x = first ? 0 : (-mapWidth - lastPlayerX); x < mapWidth; x++)
        {
            int currentHeight = nextHeight;
            nextHeight = getMountainHeight(startCell, x + 1);
            for (int y = 0; y < currentHeight; y++)
            {
                bool skip = false;
                if (
                    pastHeight < currentHeight &&
                    nextHeight < currentHeight &&
                    y != 0
                )
                {
                    skip = true;
                }

                // Si on est à la hauteur maximale de la montagne
                if (y == currentHeight - 1)
                {
                    TileBase tileToPlace = groundTile;

                    // Si on est sur le bord gauche de la montagne
                    if (
                        pastHeight < currentHeight &&
                        currentHeight <= nextHeight &&
                        y != 0 &&
                        !skip
                    )
                    {
                        tileToPlace = leftMountainTile;
                    } // Si on est sur le bord droit de la montagne
                    else if (
                        pastHeight >= currentHeight &&
                        currentHeight > nextHeight &&
                        y != 0 &&
                        !skip
                    )
                    {
                        tileToPlace = rigthMountainTile;
                    }
                    if (skip)
                    {
                        tilemap
                            .SetTile(new Vector3Int(startCell.x + x,
                                startCell.y + y - 1,
                                0),
                            tileToPlace);
                    }
                    else
                    {
                        tilemap
                            .SetTile(new Vector3Int(startCell.x + x,
                                startCell.y + y,
                                0),
                            tileToPlace);
                    }
                }
                else if (y == currentHeight - 2)
                {
                    TileBase tileToPlace = interiorTile;

                    // Si on est en dessous le bord gauche de la montagne
                    if (
                        pastHeight < currentHeight &&
                        currentHeight <= nextHeight
                    )
                    {
                        tileToPlace = leftInteriorTile;
                    } // Si on est en dessous le bord droit de la montagne
                    else if (
                        pastHeight >= currentHeight &&
                        currentHeight > nextHeight
                    )
                    {
                        tileToPlace = rightInteriorTile;
                    }

                    tilemap
                        .SetTile(new Vector3Int(startCell.x + x,
                            startCell.y + y,
                            0),
                        tileToPlace);
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
            /*for (int y = currentHeight; y < mapHeight; y++)
            {
                tilemap.SetTile(new Vector3Int(startCell.x + x, startCell.y + y, 0), mountainTile);
            }*/
            // Génère les trous de manière aléatoire
            if (Random.value < holeFrequency)
            {
                int holeSize = Random.Range(holeSizeMin, holeSizeMax + 1);
                int holeCenter = currentHeight + holeSize / 2;
                for (int y = 0; y < holeSize; y++)
                {
                    int holePosition = holeCenter - holeSize / 2 + y;
                    tilemap
                        .SetTile(new Vector3Int(startCell.x + x,
                            startCell.y + holePosition,
                            0),
                        null);

                    if (currentHeight < 1 && y == 0 && lastSwitch == null)
                    {
                        lastSwitch =
                            spawnSwitch(startCell.x + x,
                            startCell.y + holePosition);
                    }
                }
            }
            pastHeight = currentHeight;
        }
        CheckSwitchDestroy(player.position);
    }

    GameObject spawnSwitch(int x, int y)
    {
        GameObject newSwitch =
            Instantiate(switchPrefab,
            new Vector3(x - 6, y + 5, 0),
            Quaternion.identity);

        // Abonnez-vous à l'événement OnSwitchActivated
        newSwitch
            .GetComponent<SwitchController>()
            .OnSwitchActivated
            .AddListener(() => spawnBridge(new Vector3(x + (3 / 2), y + 2, 0)));

        return newSwitch;
    }

    // Étape 5: Créez la méthode SpawnBridge
    public void spawnBridge(Vector3 spawnPosition)
    {
        Instantiate(bridgePrefab, spawnPosition, Quaternion.identity);
    }

    void CheckSwitchDestroy(Vector3 playerPosition)
    {
        // Vérifiez si la position du joueur est supérieure à celle du dernier switch
        if (
            lastSwitch != null &&
            playerPosition.x > lastSwitch.transform.position.x + 10
        )
        {
            // Détruisez le dernier switch et réinitialisez la référence
            Destroy (lastSwitch);
            lastSwitch = null;
        }
    }

    int getMountainHeight(Vector3Int startCell, int x)
    {
        return Mathf
            .FloorToInt(Mathf
                .PerlinNoise((startCell.x + x) * mountainFrequency, 0) *
            mountainHeight);
    }
}
