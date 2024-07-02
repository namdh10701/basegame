
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Scripts.GD;
using UnityEngine;


public class MenuPreBattle : MonoBehaviour
{
    static Dictionary<string, int> ShapeDic = new Dictionary<string, int>()
    {
        {"0012",1},
        {"0001",2},
    };

    [Header("Prefab DragItemUI")]
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;
    // [SerializeField] ScrollRect _scrollRect;
    // [SerializeField] Canvas _canvas;
    [SerializeField] DragController _dragController;
    [SerializeField] ShipSetup _shipSetup;


    List<ItemMenu> _itemMenus = new List<ItemMenu>();

    private GridItemType _curentTab = GridItemType.Cannon;
    List<GridItemDef> selectedItems = new List<GridItemDef>();
    List<GridItemDef> inavailableItems = new List<GridItemDef>();
    [SerializeReference] GridItemReferenceHolder GridItemReferenceHolder;

    void Awake()
    {

        GDConfigLoader.Instance.OnLoaded += Initialize;
        GDConfigLoader.Instance.Load();

        _dragController.OnGridItemPlaced += OnGridItemPlaced;
        _dragController.OnGridItemUp += OnGridItemUp;
        foreach (GridItemData gridItemData in _shipSetup.Grids[0].GridItemDatas)
        {
            selectedItems.Add(gridItemData.Def);
        }
        // Initialize();
    }

    private void OnGridItemPlaced(GridItemDef reference)
    {
        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemDef.Equals(reference))
            {
                itemMenu.Disable();
            }
        }
        if (!selectedItems.Contains(reference))
        {
            selectedItems.Add(reference);
        }
    }
    private void OnGridItemUp(GridItemDef reference)
    {

        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemDef.Equals(reference))
            {
                itemMenu.Enable();
            }
        }
        if (selectedItems.Contains(reference))
        {
            selectedItems.Remove(reference);
        }
    }
    private void OnEnable()
    {
        // _scrollRect.verticalNormalizedPosition = 1;
    }

    public void SwitchTab(int tabType)
    {
        _curentTab = (GridItemType)tabType;
        Initialize();
    }

    private void Initialize()
    {
        if (_prefabItemMenu == null)
        {
            return;
        }
        if (_itemMenus.Count > 0)
        {
            RemoveItemMenus();
        }

        // var xxx = GDConfigLoader.Instance.Cannons;
        var cannons = GDConfigLoader.Instance.Cannons;
        GridItemDef[] itemList = null;
        itemList = new GridItemDef[cannons.Count];
        for (int i = 0; i < cannons.Values.Count; i++)
        {
            string name = cannons.Values.ElementAt(i).name;
            Cannon cannon = ResourceLoader.LoadCannon(name);
            if (cannon == null)
            {
                continue;
            }
            if (ResourceLoader.LoadCannon(name)) ;
            int shapeId = 0;
            ShapeDic.TryGetValue(cannons.Values.ElementAt(i).id, out shapeId);
            Debug.Log(shapeId);

            itemList[i] = new GridItemDef()
            {
                Id = cannons.Values.ElementAt(i).id,
                Type = ItemType.CANNON,
                ShapeId = shapeId,
                Name = cannons.Values.ElementAt(i).name
            };
            cannon.Def = itemList[i];
        }
        // switch (_curentTab)
        // {
        //     case GridItemType.Cannon:
        //         itemList = new GridItemDef[cannons.Count];
        //         for (int i = 0; i < cannons.Values.Count; i++)
        //         {
        //             string name = cannons.Values.ElementAt(i).name;
        //             Cannon cannon = ResourceLoader.LoadCannon(name);
        //             if (cannon == null)
        //             {
        //                 continue;
        //             }
        //             if (ResourceLoader.LoadCannon(name)) ;
        //             int shapeId = 0;
        //             ShapeDic.TryGetValue(cannons.Values.ElementAt(i).id, out shapeId);
        //             Debug.Log(shapeId);

        //             itemList[i] = new GridItemDef()
        //             {
        //                 Id = cannons.Values.ElementAt(i).id,
        //                 Type = GridItemType.Cannon,
        //                 ShapeId = shapeId,
        //                 Name = cannons.Values.ElementAt(i).name
        //             };
        //             cannon.Def = itemList[i];
        //         }

        //         break;
        //     case GridItemType.Bullet:
        //         itemList = GridItemReferenceHolder.BulletReferences;
        //         break;
        //     case GridItemType.Crew:
        //         itemList = GridItemReferenceHolder.CrewReferences;
        //         break;
        // }

        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                if (item != null)
                {
                    var temp = Instantiate(_prefabItemMenu, _content);
                    temp.Setup(item, _dragController.OnPointerDown, _dragController.OnPointerUp, selectedItems.Contains(item));

                    _itemMenus.Add(temp);
                }
            }
        }
    }

    private void RemoveItemMenus()
    {
        foreach (var item in _itemMenus)
        {
            Destroy(item.gameObject);
        }
        _itemMenus.Clear();
    }
}
