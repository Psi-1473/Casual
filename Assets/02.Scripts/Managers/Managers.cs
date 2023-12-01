using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    static Player _player = null;
    public static Player GetPlayer { get { return _player; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    DataManager _data = new DataManager();
    SummonManager _summon = new SummonManager();
    SceneManagerEx _scene = new SceneManagerEx();
    BattleManager _battle = new BattleManager();
    UpgradeManager _upgrade = new UpgradeManager();
    BuffManager _buff = new BuffManager();
    SoundManager _sound = new SoundManager();


    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SummonManager Sunmmon { get { return Instance._summon; } }
    public static SceneManagerEx SceneEx { get { return Instance._scene; } }
    public static BattleManager Battle { get { return Instance._battle; } }
    public static UpgradeManager Upgrade { get { return Instance._upgrade; } }
    public static BuffManager BuffMgr { get { return Instance._buff; } }
    public static SoundManager Sound { get { return Instance._sound; } }
   

    void Start()
    {
        Init();
	}

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
			GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
            s_instance._battle.Init();
            s_instance._buff.Init();
            s_instance._sound.Init();



            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                player = Resource.Instantiate("Player/Player");
            }
            DontDestroyOnLoad(player);
        }		
	}

    public static void Clear()
    {
        SceneEx.Clear();
        UI.Clear();
        Sound.Clear();
    }

    public static void SetPlayer(Player player)
    {
        _player = player;
    }
}
