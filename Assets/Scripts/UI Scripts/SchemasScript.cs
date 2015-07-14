using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Klid.Blocks;
using Klid;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using Klid.Properties;

public class SchemasScript : MonoBehaviour
{
    public Canvas canvas;

    public GameObject TabsParent;
    public GameObject BlocksParent;
    public GameObject BlocksGrid;
    public float ZoomSpeed = 0.01f;

    private RectTransform blocksGridRect;

    private GameObject tabPrefab;
    private GameObject blockPrefab;
    private GameObject propertyPrefab;
    private GameObject gridObjectPrefab;

    private Sprite gridCellSprite;

    private bool placing = false;
    private bool selectingBlock = false;

    Schema currentSchema = DataStorage.Schemas.Schemas[0];

    List<GameObject> blocksGameObjects = new List<GameObject>();
    List<GameObject> tabsGameObjects = new List<GameObject>(); 

    private void Start()
    {
        tabPrefab = Resources.Load<GameObject>("Prefabs/UI/Schemes/TabPrefab");
        blockPrefab = Resources.Load<GameObject>("Prefabs/UI/Schemes/BlockPrefab");
        propertyPrefab = Resources.Load<GameObject>("Prefabs/UI/Schemes/PropertyPrefab");
        gridObjectPrefab = Resources.Load<GameObject>("Prefabs/UI/Schemes/GridObjectPrefab");

        gridCellSprite = Resources.Load<Sprite>("Graphics/grid");

        blocksGridRect = BlocksGrid.GetComponent<RectTransform>();
        currentSchema.Render(BlocksGrid, gridObjectPrefab, gridCellSprite);


        tabPrefab.InstantiateEnumerable<Tab>(DataStorage.BuildingTabs, TabsParent, new Vector2(0, 0), RectTransform.Axis.Horizontal, new Vector2(0, 0), new UnityAction<GameObject, Tab>(initializeTab));
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float result = -(deltaMagnitudeDiff * ZoomSpeed);

            blocksGridRect.localScale = new Vector3(blocksGridRect.localScale.x + result, blocksGridRect.localScale.y + result, blocksGridRect.localScale.z);
        }

        if (Input.touchCount == 1)
        {
            if (placing)
            {
                Touch touchZero = Input.GetTouch(0);
                if (touchZero.phase == TouchPhase.Ended)
                {
                    if (selectingBlock)
                    {
                        selectingBlock = false;
                        Debug.Log("Stopped selecting");
                    }
                    else
                    {
						Rect rect = blocksGridRect.rect;
						Vector2 relativeTouchPosition = touchZero.position - new Vector2(blocksGridRect.anchoredPosition.x * blocksGridRect.localScale.x * canvas.scaleFactor, blocksGridRect.anchoredPosition.y * blocksGridRect.localScale.x * canvas.scaleFactor);
                        Debug.Log("Relative " + relativeTouchPosition.x + "x " + relativeTouchPosition.y + "y");
                        if (relativeTouchPosition.x > 0 && relativeTouchPosition.x < blocksGridRect.rect.width && relativeTouchPosition.y > 0 && relativeTouchPosition.y < blocksGridRect.rect.height)
                        {
                            // block can be placed
                            Vector2 relativeScaledTouchPosition = relativeTouchPosition * blocksGridRect.localScale.x * canvas.scaleFactor;
                            //Debug.Log("Place at" + relativeScaledTouchPosition.x + "x " + relativeScaledTouchPosition.y + "y");
                        }
                        else
                        {
                            // invalid position (outside of the panel)
                            placing = false;
                            Debug.Log("Cannot be placed");
                        }
                    }
                }

            }
            else
            {
                // Store touch
                Touch touchZero = Input.GetTouch(0);

                if (touchZero.position.x < Screen.width - BlocksParent.GetComponent<RectTransform>().rect.width * canvas.scaleFactor)
                {
                    // Find the position in the previous frame of each touch.
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;

                    Vector2 touchDiff = touchZero.position - touchZeroPrevPos;

                    blocksGridRect.anchoredPosition += touchDiff;
                }
            }
        }
    }


    private void initializeTab(GameObject tabGameObject, Tab tab)
    {
        Button button = tabGameObject.GetComponent<Button>();
        Tab tabCopy = tab;
        button.onClick.AddListener(new UnityAction(() => generateBlocksGameObjects(tabCopy)));

        Image image = tabGameObject.transform.FindChild("Image").GetComponentInChildren<Image>();
        image.sprite = tab.Sprite;

        tabsGameObjects.Add(tabGameObject);
    }

    private void generateBlocksGameObjects(Tab selectedTab)
    {
        foreach (GameObject gameObject in blocksGameObjects)
            GameObject.DestroyImmediate(gameObject);
        blocksGameObjects.Clear();

        blockPrefab.InstantiateEnumerable<Block>(from block in DataStorage.CraftedBlocks.CraftedBlocks where block.Tab == selectedTab select block, BlocksParent, new Vector2(0, -10), RectTransform.Axis.Vertical, new Vector2(0, 0), new UnityAction<GameObject, Block>(initializeBlock));
    }

    private void initializeBlock(GameObject blockGameObject, Block block)
    {
        Image image = blockGameObject.transform.FindChild("BlockImage").GetComponent<Image>();
        image.sprite = block.Sprite;

        GameObject propertiesParent = blockGameObject.transform.FindChild("BlockProperties").gameObject;
        propertyPrefab.InstantiateEnumerable<Property>(block.Properties, propertiesParent, new Vector2(0, 0), RectTransform.Axis.Vertical, new Vector2(), new UnityAction<GameObject, Property>(initializeProperty));

        Block blockCopy = block;
        GameObject blockGOCopy = blockGameObject;
        
        Button button = blockGameObject.GetComponent<Button>();
        button.onClick.AddListener(new UnityAction(() => blockButtonPressed(blockCopy, blockGOCopy)));
        
        blocksGameObjects.Add(blockGameObject);
    }

    private void initializeProperty(GameObject propertyGameObject, Property property)
    {
        Image image = propertyGameObject.transform.FindChild("PropertyImage").GetComponent<Image>();
        image.sprite = property.Sprite;

        Text name = propertyGameObject.transform.FindChild("PropertyName").GetComponent<Text>();
        name.text = property.Value.ToString();
    }

    private void blockButtonPressed(Block block, GameObject blockGO)
    {
        placing = true;
        selectingBlock = true;
        Debug.Log(block.Name);
    }
}
