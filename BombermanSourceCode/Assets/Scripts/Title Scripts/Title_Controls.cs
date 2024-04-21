using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Title_Controls : MonoBehaviour
{
    public static bool Continue_Load;
    public bool Continue;
    public bool Manual;
    public int Menu_index = -1;
    public int Manual_index = 0;
    public int Manual_Max_Range = 8;
    public int Enter_index = 0;
    public int Continue_index = 0;
    [Space(2f)]
    [Header("Load/Save Data")]
    public string saveDirectory = "Highscore";
    public string saveName = "Highscore_DO_NOT_DELETE";
    public Points_Saved Saving_P;
    public Points_Loaded Loading_P;
    public TextMeshProUGUI Text_Highscore;
    public TextMeshProUGUI Text_Highscore_Shadow;
    [Space(2f)]
    [Header("Starting Segment")]
    public GameObject Start_Segment;
    public GameObject Start_Segment_Icons;
    public Animator Explosion;
    public RainbowText_V3 Remix;
    public GameObject Cursor;
    public AudioSource Cursor_SFX;
    public AudioSource Wrong_Sfx;
    public Transform Cursor_Pos;
    public Transform[] Cursor_Goto_Pos;

    [Space(2f)]
    [Header("Manual Segment")]
    public GameObject Manual_Segment;
    public GameObject Manual_Segment_Icons;
    public TextMeshProUGUI Text_Curr;
    public TextMeshProUGUI Text_Max;
    public TextMeshProUGUI Left_Arrow;
    public TextMeshProUGUI Right_Arrow;
    public GameObject[] Text_Sets;
    public GameObject[] Icon_Sets;

    [Space(2f)]
    [Header("Continue Segment")]
    private int Goto_Stage;
    public int Letter_index = 65;
    public int Curr_Length;
    public string Typed_Word;
    public GameObject Continue_Segment;
    public Transform Square;
    public GameObject Square_Obj;
    public TextMeshProUGUI[] Letters;
    public TextMeshProUGUI[] Letters_Shadows;
    public Transform[] Square_Pos;
    private string[] Passwords =
       {"NMIHPPBPCAFHABDPCPCH", "HIJDIJFJDLHFLOPDJDJN", "BAJDINANMJGGCPOOLOLG", "DJOLBGLGKGJAHIEMNMNN", "NMKGDDONMHLCGKKGKGKJ", "ABGKKBPHILHFLOPCPCPC", "FEBABGLEFLHFLOPCPCPA","HIFEMIIABJGGCPOBABAN","NMEFPHCMNJGGCPOBABAF", "JDGKKBPHILHFLOPGKGKL",
        "HIPCOHCMNLHFLOPEFEFG", "ABJDIFJKGGJAHIEPCPCN", "JDBABANOLJGGCPODJDJF", "ABNMKNAIHFAJNMMKGKGF", "ABIHPGLEFCNNJDBEFEFN","JDDJOIIOLCNNJDBABOLH", "JDNMKLGHILHFLOPGKEFH", "DJABEKMPCFAJNMMOLFEL", "FEGKKJFNMAFHABDABOLN", "NMKGDDOIHJGGCPONMIHN",
        "NMKGDDOIHJGGCPONMIHN", "NMCPIIIOLFAJNMMGKEFF", "NMPCOIIOLCNNJDBBAHIJ", "NMGKKEEHILHFLOPPCGKL", "HIKGDODCPGJAHIEPCGKJ", "ABHIMGLBANCLFEINMIHH", "MNGKKDOOLGJAHIEKGCPC", "OLDJOIIKGLHFLOPEFLOL", "IHJDIKMEFNCLFEINMIHF", "IHDJOIIKGLHFLOPMNJDA",
        "DJJDIDOOLFAJNMMEFLOC","IHIHPBPCPNCBOLIHIJDH", "OLFEMANMNFADDJMABFEF", "MNDJOODJDHLPPCKBAMNA", "DJABEMKMNNCMIHIMNDJC", "BADJOIIIHAFDDJDIHOLA", "DJFEMPBPCGJKEFEEFBAC", "DJKGDIIIHJGBOLOABFEH", "DJCPIODFECNOBABABFEN","IHEFPPBGKFAIMNMOLKGJ",
        "IHLOEHCMNNCMIHIIHOLJ", "DJEFPHCMNJGBOLOABFEH", "MNGKKIIOLGJKEFEKGPCJ", "BAPCOMKDJJGBOLODJIHJ", "OLNMKDOIHFAIMNMGKLOF", "OLIHPMKNMFAIMNMABFEH", "OLABEMKNMCNOBABPCEFL","OLOLBFJGKGJKEFEFEPCL", "OLFEMFJGKLHPPCPLOMNL", "NMABEKMKGNCLFEIIHFEL"};

    private char The_Letter;
    private bool Letter_Follow = true;
    private bool BlockTimer;
    public float Timer = 0.3f;
    public float Timer_Max = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        Explosion.enabled = false;
        Checking_Data();
        if (Loading_P.The_Curr_Highscore() > 0)
        {
            Text_Highscore.text = Loading_P.The_Curr_Highscore().ToString();
            Text_Highscore_Shadow.text = Text_Highscore.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text_Curr.text = Manual_index.ToString();
        Text_Max.text = Manual_Max_Range.ToString();

        Square.position = Square_Pos[Curr_Length].position;

        // Makes the shadow text same as the regular text for the password input
        if (Letter_Follow)
        {
            Letters_Shadows[0].text = Letters[0].text;
            Letters_Shadows[1].text = Letters[1].text;
            Letters_Shadows[2].text = Letters[2].text;
            Letters_Shadows[3].text = Letters[3].text;
            Letters_Shadows[4].text = Letters[4].text;
            Letters_Shadows[5].text = Letters[5].text;
            Letters_Shadows[6].text = Letters[6].text;
            Letters_Shadows[7].text = Letters[7].text;
            Letters_Shadows[8].text = Letters[8].text;
            Letters_Shadows[9].text = Letters[9].text;
            Letters_Shadows[10].text = Letters[10].text;
            Letters_Shadows[11].text = Letters[11].text;
            Letters_Shadows[12].text = Letters[12].text;
            Letters_Shadows[13].text = Letters[13].text;
            Letters_Shadows[14].text = Letters[14].text;
            Letters_Shadows[15].text = Letters[15].text;
            Letters_Shadows[16].text = Letters[16].text;
            Letters_Shadows[17].text = Letters[17].text;
            Letters_Shadows[18].text = Letters[18].text;
            Letters_Shadows[19].text = Letters[19].text;
        }

        if (BlockTimer)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                BlockTimer = false;
                Timer = Timer_Max;
                Square_Obj.SetActive(true);
            }
        }

        // Makes the left arrrow disappear if it's at the start of the manual index
        if (Manual_index == 0)
        {
            Left_Arrow.enabled = false;
        }
        else
        {
            Left_Arrow.enabled = true;
        }

        // Makes the right arrrow disappear if it's at the end of the manual index
        if (Manual_index == Manual_Max_Range)
        {
            Right_Arrow.enabled = false;
        }
        else
        {
            Right_Arrow.enabled = true;
        }

        // Increase the index and more depending if Manual, Continue, or the main part is selected
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (!Manual && !Continue)
            {
                Menu_index--;
                if (Menu_index > 1)
                {
                    Menu_index = 1;
                }
                else if (Menu_index == 1)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[2].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index == 0)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[1].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index == -1)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[0].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index < -1)
                {
                    Menu_index = -1;
                }
            }
            else if (Manual)
            {
                if (Manual_index != 0)
                {
                    Text_Sets[Manual_index].SetActive(false);
                    Icon_Sets[Manual_index].SetActive(false);
                }

                Manual_index--;
                if (Manual_index > Manual_Max_Range)
                {
                    Manual_index = Manual_Max_Range;
                }
                else if (Manual_index < 0)
                {
                    Manual_index = 0;
                }
                else
                {
                    Cursor_SFX.Play();
                }
                Left_Arrow.text = "<#FED70A><";
                Text_Sets[Manual_index].SetActive(true);
                Icon_Sets[Manual_index].SetActive(true);
            }
            else if (Continue)
            {
                Timer = Timer_Max;
                Square_Obj.SetActive(false);
                Letter_index--;
                if (Letter_index > 80)
                {
                    Letter_index = 65;
                }
                else if (Letter_index < 65)
                {
                    Letter_index = 80;
                }
                The_Letter = Convert.ToChar(Letter_index);
                Letters[Curr_Length].text = The_Letter.ToString();
            }
        }

        // Increase the index and more depending if Manual, Continue, or the main part is selected
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (!Manual && !Continue)
            {
                Menu_index++;
                if (Menu_index > 1)
                {
                    Menu_index = 1;
                }
                else if (Menu_index == 1)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[2].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index == 0)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[1].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index == -1)
                {
                    Cursor_Pos.position = Cursor_Goto_Pos[0].position;
                    Cursor_SFX.Play();
                }
                else if (Menu_index < -1)
                {
                    Menu_index = -1;
                }
            }
            else if (Manual)
            {
                if (Manual_index != Manual_Max_Range)
                {
                    Text_Sets[Manual_index].SetActive(false);
                    Icon_Sets[Manual_index].SetActive(false);
                }

                Manual_index++;
                if (Manual_index > Manual_Max_Range)
                {
                    Manual_index = Manual_Max_Range;
                }
                else if (Manual_index < 0)
                {
                    Manual_index = 0;
                }
                else
                {
                    Cursor_SFX.Play();
                }
                Right_Arrow.text = "<#FED70A>>";
                Text_Sets[Manual_index].SetActive(true);
                Icon_Sets[Manual_index].SetActive(true);
            }
            else if (Continue)
            {
                Timer = Timer_Max;
                Square_Obj.SetActive(false);
                Letter_index++;
                if (Letter_index > 80)
                {
                    Letter_index = 65;
                }
                else if (Letter_index < 65)
                {
                    Letter_index = 80;
                }
                The_Letter = Convert.ToChar(Letter_index);
                Letters[Curr_Length].text = The_Letter.ToString();
            }
        }

        // Lets Skips up to 5 letters when inputing the password
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (Continue)
            {
                Timer = Timer_Max;
                Square_Obj.SetActive(false);
                Letter_index += 5;
                if (Letter_index > 80)
                {
                    Letter_index = 65;
                }
                else if (Letter_index < 65)
                {
                    Letter_index = 80;
                }
                The_Letter = Convert.ToChar(Letter_index);
                Letters[Curr_Length].text = The_Letter.ToString();
            }
        }

        // Lets Skips down to 5 letters when inputing the password
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (Continue)
            {
                Timer = Timer_Max;
                Square_Obj.SetActive(false);
                Letter_index -= 5;
                if (Letter_index > 80)
                {
                    Letter_index = 65;
                }
                else if (Letter_index < 65)
                {
                    Letter_index = 80;
                }
                The_Letter = Convert.ToChar(Letter_index);
                Letters[Curr_Length].text = The_Letter.ToString();
            }
        }

        // Confirm various parts depending on what's selected
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            if (!Manual && !Continue)
            {
                if (Menu_index == -1)
                {
                    SceneManager.LoadScene(1);
                }
                else if (Menu_index == 0)
                {
                    Continue = true;
                    Manual = false;
                    Continue_Segment.SetActive(true);
                    Manual_Segment.SetActive(false);
                    Manual_Segment_Icons.SetActive(false);
                    Start_Segment.SetActive(false);
                    Start_Segment_Icons.SetActive(false);
                }
                else if (Menu_index == 1)
                {
                    Text_Sets[Manual_index].SetActive(true);
                    Icon_Sets[Manual_index].SetActive(true);
                    Manual = true;
                    Continue = false;
                    Manual_Segment.SetActive(true);
                    Manual_Segment_Icons.SetActive(true);
                    Start_Segment.SetActive(false);
                    Continue_Segment.SetActive(false);
                    Start_Segment_Icons.SetActive(false);
                }
            }
            else if (Continue)
            {
                if (!string.IsNullOrEmpty(Letters[Curr_Length].text) || Letter_index != 64)
                {
                    if (Curr_Length <= 18)
                    {
                        Typed_Word += Letters[Curr_Length].text;
                        Curr_Length++;
                    }
                    else
                    {
                        Typed_Word += Letters[Curr_Length].text;
                        if (Is_Password())
                        {
                            // Load Stage
                            Continue_Load = true;
                            Lives_System.Stages = Goto_Stage;
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            Typed_Word = Typed_Word.Remove(Typed_Word.Length - 1);
                            Wrong_Sfx.Play();
                        }
                    }
                    Letter_index = 64;
                }
            }
        }

        // Used to go back to the main menu part
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (Manual)
            {
                Text_Sets[Manual_index].SetActive(false);
                Icon_Sets[Manual_index].SetActive(false);
                Manual_Segment.SetActive(false);
                Manual_Segment_Icons.SetActive(false);
                Start_Segment.SetActive(true);
                Start_Segment_Icons.SetActive(true);
                Manual_index = 0;
                Manual = false;
                Right_Arrow.text = "<#FFFFFF>>";
                Left_Arrow.text = "<#FFFFFF><";
                Remix.Invoke("Start", 0);
            }
            else if (Continue)
            {
                ClearAll();
                Continue = false;
                Continue_Segment.SetActive(false);
                Start_Segment.SetActive(true);
                Start_Segment_Icons.SetActive(true);
                Remix.Invoke("Start", 0);
            }
        }

        // Ease the current letter for the password
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Continue)
            {
                if (Curr_Length > 0)
                {
                    Typed_Word = Typed_Word.Remove(Typed_Word.Length - 1);
                    Letters[Curr_Length].text = "";
                    Curr_Length--;
                }
            }
        }

        // Used to hightlight the arrows for manual and start the time for the password blinking effect
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            if (Manual)
            {
                Right_Arrow.text = "<#FFFFFF>>";
                Left_Arrow.text = "<#FFFFFF><";
            }
            else if (Continue)
            {
                BlockTimer = true;
            }
        }

        // start the time for the password blinking effect
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (Continue)
            {
                BlockTimer = true;
            }
        }
    }

    // Resets everything 
    public void ClearAll()
    {
        Letter_index = 64;
        Curr_Length = 0;
        Typed_Word = "";
        BlockTimer = false;
        Timer = Timer_Max;
        for (int i = 0; i < Letters.Length; i++)
        {
            Letters[i].text = "";
        }
    }

    // Checks to see if password was accepted
    public bool Is_Password()
    {
        for (int i = 0; i < Passwords.Length; i++)
        {
            if (Typed_Word == Passwords[i])
            {
                Goto_Stage = (i + 1);
                return true;
            }
        }
        return false;
    }

    // Checks to see if the Highscore file is there 
    public void Checking_Data()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Loading_P.The_Highscore.Curr_Highscore = 0;
            Text_Highscore.text = "00";
            Saving_P.The_Highscore.Curr_Highscore = 0;
        }
        if (!File.Exists(saveDirectory + "/" + saveName + ".bin"))
        {
            Loading_P.The_Highscore.Curr_Highscore = 0;
            Text_Highscore.text = "00";
            Saving_P.The_Highscore.Curr_Highscore = 0;
        }
    }
}
