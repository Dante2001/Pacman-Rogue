using UnityEngine;
using System.Collections;

public class LevelTiler : MonoBehaviour {

    const char ITEM = 'i';
    const char BORDER = 'b';
    const char COIN = 'c';

    public char[,] level;

    private int height;
    private int width;

    private float tileHeight;
    private float tileWidth;
    private float fillWidth;
    private float fillHeight;

    public GameObject coin;
    public GameObject border;
    public GameObject borderRight;
    public GameObject borderCorner;
    public GameObject borderTopBot;
    public GameObject borderNoBot;
    public GameObject borderClosed;
    public GameObject borderFillCorner;
    public GameObject item;

    private GameObject lastInst;
    private GameObject levelParent;

    // Use this for initialization
    void Start() {
        levelParent = new GameObject();
        levelParent.name = "Level";
        lastInst = null;
        /*level = new char[70, 53];
        for (int i = 0; i < 70; i++)
            for (int j = 0; j < 53; j++)
            {
                if (Random.Range(0, 6) < 3)
                    level[i, j] = COIN;
                else
                    level[i, j] = BORDER;
            }/*
        /*level = new char[,]
        {
            { BORDER, COIN, COIN, COIN, BORDER },
            { BORDER, BORDER, BORDER, COIN, BORDER },
            {BORDER, COIN, BORDER, COIN, COIN },
            { BORDER, COIN, COIN, COIN, BORDER },
            { BORDER, COIN, COIN, COIN, BORDER }
        };*/
        /*level = new char[,]
        {
            { BORDER, COIN, COIN, COIN, COIN },
            { BORDER, BORDER, BORDER, BORDER, BORDER },
            {BORDER, COIN, BORDER, COIN, BORDER },
            { BORDER, COIN, COIN, COIN, BORDER },
            { BORDER, COIN, COIN, COIN, BORDER }
        };*/
        /*
        level = new char[,]
        {
            {COIN, COIN, COIN, BORDER, COIN, COIN, BORDER,BORDER,BORDER, COIN },
            {COIN, BORDER, COIN, BORDER, COIN, COIN, BORDER, COIN, BORDER, COIN },
            {BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, BORDER, COIN, BORDER, COIN },
            {COIN,COIN, BORDER, COIN, COIN, COIN, BORDER, BORDER, BORDER,COIN },
            {COIN, COIN, COIN, COIN, COIN, COIN, BORDER, BORDER, BORDER, COIN }
        };
        */
        /*
        level = new char[,]
        {
            { COIN, COIN,COIN,COIN,COIN,COIN,COIN,COIN,BORDER,COIN,COIN,COIN},
            {COIN, COIN, COIN, COIN, BORDER, COIN, COIN, BORDER,BORDER,BORDER, COIN,COIN },
            {COIN, COIN, COIN, BORDER, BORDER, BORDER, COIN, BORDER, COIN, BORDER, COIN,COIN },
            {COIN, BORDER, BORDER, COIN, COIN, COIN, BORDER, BORDER, COIN, BORDER, BORDER,COIN },
            {COIN, COIN,BORDER, COIN, COIN, COIN, COIN, BORDER, BORDER, BORDER,COIN,COIN },
            {COIN, COIN, COIN, COIN, COIN, COIN, COIN, COIN, COIN, COIN, COIN,COIN },
            { COIN, BORDER,COIN,COIN,BORDER,BORDER,COIN,COIN,COIN,BORDER,COIN,COIN},
            { COIN, BORDER,BORDER,COIN,BORDER,COIN,COIN,COIN,BORDER,BORDER,COIN,COIN},
            { COIN, COIN,COIN,COIN,COIN,COIN,COIN,COIN,COIN,COIN,COIN,COIN}
        };*/
        /*level = new char[,]
        {
            { BORDER, COIN, COIN, COIN, BORDER },
            { BORDER, COIN, COIN, BORDER, BORDER },
            { BORDER, BORDER, BORDER, BORDER, COIN },
            {BORDER, COIN, COIN, COIN, BORDER },
            { BORDER, COIN, COIN, COIN, BORDER }
        };*/
        //TileLevel();
        GameManager.TestLevel3();
        level = GameManager.level;
        TileLevel();
    }

    public void SetLevel(char[,] lvl)
    {
        level = lvl;
    }

    // Call this to set up the tiles for the current level
    public void TileLevel()
    {
        width = level.GetLength(1);
        height = level.GetLength(0);
        SpriteRenderer renderer = coin.GetComponent<SpriteRenderer>();
        tileHeight = renderer.sprite.bounds.extents.y * 2 * coin.transform.localScale.y;
        tileWidth = renderer.sprite.bounds.extents.x * 2 * coin.transform.localScale.x;
        renderer = borderFillCorner.GetComponent<SpriteRenderer>();
        fillWidth = renderer.sprite.bounds.extents.x * 2 * coin.transform.localScale.x;
        fillHeight = renderer.sprite.bounds.extents.y * 2 * coin.transform.localScale.y;
        int x = 0;// -width / 2;
        int y = 0;// height / 2;
        Vector3 pos = Vector3.zero;
        pos.x = x * tileWidth + tileWidth / 2;
        pos.y = y * tileHeight + tileHeight / 2;

        GameManager.tileHeight = tileHeight;
        GameManager.tileWidth = tileWidth;
        GameManager.tileZeroZeroPosition = pos;

        Vector3 startPos = pos;
        for (y = 0; y < height; y++)
        {
            for (x = 0; x < width; x++)
            {
                switch (level[y, x])
                {
                    case (COIN):
                        lastInst = (GameObject)Instantiate(coin, pos, Quaternion.identity);
                        break;
                    case (BORDER):
                        InstantiateBorderTile(x, y, pos);
                        break;
                    case (ITEM):
                        lastInst = (GameObject)Instantiate(item, pos, Quaternion.identity);
                        break;
                }
                pos.x += tileWidth;
            }
            lastInst = null;
            pos.x = startPos.x;
            pos.y -= tileHeight;
        }

    }

    private void InstantiateBorderTile(int x, int y, Vector3 pos)
    {
        GameObject curInst;
        char left, right, top, bot;
        left = right = top = bot = BORDER;
        if (x - 1 >= 0) left = level[y, x - 1];
        if (x + 1 < width) right = level[y, x + 1];
        if (y - 1 >= 0) top = level[y - 1, x];
        if (y + 1 < height) bot = level[y + 1, x];

        if (left == BORDER && right == BORDER && top == BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(border, pos, Quaternion.identity);
        else if (left == BORDER && right == BORDER && top == BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderRight, pos, Quaternion.AngleAxis(90, Vector3.back));
        else if (left == BORDER && right == BORDER && top != BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderRight, pos, Quaternion.AngleAxis(270, Vector3.back));
        else if (left == BORDER && right != BORDER && top == BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderRight, pos, Quaternion.identity);
        else if (left != BORDER && right == BORDER && top == BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderRight, pos, Quaternion.AngleAxis(180, Vector3.back));
        else if (left == BORDER && right == BORDER && top != BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderTopBot, pos, Quaternion.identity);
        else if (left != BORDER && right != BORDER && top == BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderTopBot, pos, Quaternion.AngleAxis(270, Vector3.back));
        else if (left == BORDER && right != BORDER && top == BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderCorner, pos, Quaternion.AngleAxis(90, Vector3.back));
        else if (left != BORDER && right == BORDER && top == BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderCorner, pos, Quaternion.AngleAxis(180, Vector3.back));
        else if (left == BORDER && right != BORDER && top != BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderCorner, pos, Quaternion.identity);
        else if (left != BORDER && right == BORDER && top != BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderCorner, pos, Quaternion.AngleAxis(270, Vector3.back));
        else if (left == BORDER && right != BORDER && top != BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderNoBot, pos, Quaternion.AngleAxis(90, Vector3.back));
        else if (left != BORDER && right == BORDER && top != BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderNoBot, pos, Quaternion.AngleAxis(270, Vector3.back));
        else if (left != BORDER && right != BORDER && top == BORDER && bot != BORDER)
            curInst = (GameObject) Instantiate(borderNoBot, pos, Quaternion.AngleAxis(180, Vector3.back));
        else if (left != BORDER && right != BORDER && top != BORDER && bot == BORDER)
            curInst = (GameObject) Instantiate(borderNoBot, pos, Quaternion.identity);
        else
            curInst = (GameObject) Instantiate(borderClosed, pos, Quaternion.identity);

        // adjust the previous (or current) filler tile
        InstantiateCornersOnFillTiles(lastInst, curInst);
        InstantiateExtraFillCorners(lastInst, curInst);
        lastInst = curInst;
    }

    private void InstantiateCornerFillTopLeft(GameObject origin)
    {
        Vector3 pos = origin.transform.position;
        pos.x -= tileWidth / 2 - fillWidth / 2;
        pos.y += tileHeight / 2 - fillHeight / 2;
        Instantiate(borderFillCorner, pos, Quaternion.identity);
    } // END InstantiateCornerFillTopLeft

    private void InstantiateCornerFillTopRight(GameObject origin)
    {
        Vector3 pos = origin.transform.position;
        pos.x += tileWidth / 2 - fillWidth / 2;
        pos.y += tileHeight / 2 - fillHeight / 2;
        Instantiate(borderFillCorner, pos, Quaternion.identity);
    } // END InstantiateCornerFillTopRight

    private void InstantiateCornerFillBotLeft(GameObject origin)
    {
        Vector3 pos = origin.transform.position;
        pos.x -= tileWidth / 2 - fillWidth / 2;
        pos.y -= tileHeight / 2 - fillHeight / 2;
        Instantiate(borderFillCorner, pos, Quaternion.identity);
    } // END InstantiateCornerFillBotLeft

    private void InstantiateCornerFillBotRight(GameObject origin)
    {
        Vector3 pos = origin.transform.position;
        pos.x += tileWidth / 2 - fillWidth / 2;
        pos.y -= tileHeight / 2 - fillHeight / 2;
        Instantiate(borderFillCorner, pos, Quaternion.identity);
    } // END InstantiateCornerFillBotRight

    private void InstantiateCornersOnFillTiles(GameObject last, GameObject cur)
    {
        if (last == null || !last.name.Contains("Border") ||
            !(last.name.Contains("Fill") | cur.name.Contains("Fill")))
            return;

        if (cur.name.Contains("Fill"))
        {
            if (last.name.StartsWith(borderTopBot.name) || last.name.StartsWith(borderNoBot.name))
            {
                InstantiateCornerFillBotLeft(cur);
                InstantiateCornerFillTopLeft(cur);
            }
            else if (last.name.StartsWith(borderCorner.name))
            {
                if (last.transform.eulerAngles.z == 180)
                {
                    InstantiateCornerFillBotLeft(cur);
                }
                else if (last.transform.eulerAngles.z == 90)
                {
                    InstantiateCornerFillTopLeft(cur);
                }
            }
            else if (last.name.StartsWith(borderRight.name))
            {
                if (last.transform.eulerAngles.z == 270)
                {
                    InstantiateCornerFillBotLeft(cur);
                }
                else if (last.transform.eulerAngles.z == 90)
                {
                    InstantiateCornerFillTopLeft(cur);
                }
            }
        } // end if (cur.name.Contains("Fill"))
        else if (last.name.Contains("Fill"))
        {
            if (cur.name.StartsWith(borderTopBot.name) || cur.name.StartsWith(borderNoBot.name))
            {
                InstantiateCornerFillBotRight(last);
                InstantiateCornerFillTopRight(last);
            }
            else if (cur.name.StartsWith(borderCorner.name))
            {
                if (cur.transform.eulerAngles.z == 270)
                {
                    InstantiateCornerFillBotRight(last);
                }
                else if (cur.transform.eulerAngles.z == 0)
                {
                    InstantiateCornerFillTopRight(last);
                }
            }
            else if (cur.name.StartsWith(borderRight.name))
            {
                if (cur.transform.eulerAngles.z == 270)
                {
                    InstantiateCornerFillBotRight(last);
                }
                else if (cur.transform.eulerAngles.z == 90)
                {
                    InstantiateCornerFillTopRight(last);
                }
            }
        } // end if (last.name.Cotains("Fill"))
    } // END InstantiateExtraCornersOnFIllTiles

    private void InstantiateExtraFillCorners(GameObject last, GameObject cur)
    {
        if (last == null || last.name.Contains("Fill") || cur.name.Contains("Fill"))
            return;

        if (last.name.StartsWith(borderTopBot.name))
        {
            if (cur.name.StartsWith(borderCorner.name))
            {
                if (cur.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }
            else if (cur.name.StartsWith(borderRight.name))
            {
                Debug.Log(cur.transform.eulerAngles);
                if (cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }
        } // end if (last.name.StartsWith(borderTopBot.name))
        else if (cur.name.StartsWith(borderTopBot.name))
        {
            if (last.name.StartsWith(borderCorner.name))
            {
                if (last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 180)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }
            else if (last.name.StartsWith(borderRight.name))
            {
                if (last.transform.eulerAngles.z == 180)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }
        }// end if (cur.name.StartsWith(borderTopBot.name))
        else if (cur.name.StartsWith(borderRight.name))
        {
            if (last.name.StartsWith(borderCorner.name))
            {
                if (last.transform.eulerAngles.z == 180 && cur.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    pos.x += fillWidth;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 + fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    pos.x -= fillWidth;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 0)
                {
                    if (last.transform.eulerAngles.z == 180)
                        InstantiateCornerFillBotLeft(cur);
                    else if (last.transform.eulerAngles.z == 90)
                        InstantiateCornerFillTopLeft(cur);
                }
            }// end if last.name.StartsWith(borderCOrner.name))
            else if (last.name.StartsWith(borderRight.name))
            {
                if (last.transform.eulerAngles.z == 180 && cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 180 && cur.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 270 && cur.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 270 && cur.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.x -= fillWidth;
                    pos.y -= tileHeight - fillHeight;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }// end if (last.name.StartsWith(borderRight.name))
            else if (last.name.StartsWith(borderNoBot.name))
            {
                if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }

                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }

        }// end if (cur.name.StartsWith(borderRight.name)
        else if (cur.name.StartsWith(borderCorner.name))
        {
            if (last.name.StartsWith(borderRight.name))
            {
                if (last.transform.eulerAngles.z == 270 && cur.transform.eulerAngles.z == 0)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    pos.x += fillWidth;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 90 && cur.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    pos.x -= fillWidth;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (last.transform.eulerAngles.z == 180)
                {
                    if (cur.transform.eulerAngles.z == 270)
                        InstantiateCornerFillBotRight(last);
                    else if (cur.transform.eulerAngles.z == 0)
                        InstantiateCornerFillTopRight(last);
                }
            }// end if (last.name.StartsWith(borderRight.name))
            else if (last.name.StartsWith(borderNoBot.name))
            {
                if (cur.transform.eulerAngles.z == 0 && last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = cur.transform.position;
                    pos.x -= tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            }// end if (last.name.StartsWith(borderNoBot.name))
            else if (last.name.StartsWith(borderCorner.name))
            {
                if (cur.transform.eulerAngles.z == 0 && last.transform.eulerAngles.z == 180)
                {
                    InstantiateCornerFillTopRight(last);
                    InstantiateCornerFillBotLeft(cur);
                }
                else if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 90)
                {
                    InstantiateCornerFillBotRight(last);
                    InstantiateCornerFillTopLeft(cur);
                }
            }
        }// end if (cur.name.StartsWith(borderCorner.name))
        else if (cur.name.StartsWith(borderNoBot.name))
        {
            if (last.name.StartsWith(borderRight.name))
            {
                if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 270)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 180)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                    pos.y -= tileHeight - fillHeight;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
            } // end if (last.name.StartsWith(borderRight.name))
            else if (last.name.StartsWith(borderCorner.name))
            {
                if (cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 180)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y += tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }
                else if(cur.transform.eulerAngles.z == 270 && last.transform.eulerAngles.z == 90)
                {
                    Vector3 pos = last.transform.position;
                    pos.x += tileWidth / 2 - fillWidth / 2;
                    pos.y -= tileHeight / 2 - fillHeight / 2;
                    Instantiate(borderFillCorner, pos, Quaternion.identity);
                }

            }// end if (last.name.StartsWith(borderCorner.name))
        }
        // TODO: finish coner x right shit -- FINISHED?
        // TODO: add all but button x right and corner shit -- FINISHED??
        // IDEA: have extra sprites for right with, one connector, two connector, one connector
        //       have extra sprites for corner with, one connector
    }
}
