using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class MetricHolePoint
{
    public Vector3 Start;
    public Vector3 End;
    public string HoleAxis;
    public enum HoleCubeNum {One,Three};
    public HoleCubeNum HoleCubeIndex;

    public enum VisibleByCamEnum { Left,Right,Top,Bot};
    public VisibleByCamEnum VisisbleByCam;
    //HoleCubeIndex

    public MetricHolePoint(Vector3 start, Vector3 end, String holeaxis, HoleCubeNum holeCubeIndex, VisibleByCamEnum visisbleByCam, GameObject parentPoint)
    {
        
        Start = start;
        End = end;
        HoleAxis = holeaxis;
        HoleCubeIndex = holeCubeIndex;
        VisisbleByCam = visisbleByCam;
     
        BuildHoleCubePointPositionInform(start, end, holeCubeIndex, visisbleByCam,parentPoint);



    }



    public void BuildHoleCubePointPositionInform(Vector3 startPos, Vector3 endPos, MetricHolePoint.HoleCubeNum holeCubeNum, MetricHolePoint.VisibleByCamEnum visCamera,GameObject ParentPoint)
    {
        BanyaGeneralInfoScript bs = GameObject.FindObjectOfType<BanyaGeneralInfoScript>();
        float cutMetricLinesAtEndAndStart = bs.MetricLinesCone.GetComponentInChildren<Renderer>().bounds.size.z; //0.095f;

   


        GameObject someMetricPointPosInformGO = new GameObject();
        someMetricPointPosInformGO.name = ParentPoint.name+ "PosInformGO";
        


        ///******MetricLinesGO.Add(someMetricPointPosInformGO);
        GameObject someCubePointPosINformMeshText = new GameObject();
        someCubePointPosINformMeshText.transform.parent = ParentPoint.transform;
        someMetricPointPosInformGO.transform.parent = ParentPoint.transform;

        someCubePointPosINformMeshText.name = "Holecube "+holeCubeNum.ToString() + ", camera " + visCamera.ToString() + " " + ParentPoint.ToString();
        //******bs.MetricLines_DistanceMeshTextGO.Add(someCubePointPosINformMeshText);
        someCubePointPosINformMeshText.AddComponent<TextMesh>();
        if ((visCamera==MetricHolePoint.VisibleByCamEnum.Left)|| (visCamera == MetricHolePoint.VisibleByCamEnum.Right))
            someCubePointPosINformMeshText.GetComponent<TextMesh>().text = "x=" + (Mathf.RoundToInt(startPos.x * 1000)).ToString() + "\ny=" + (Mathf.RoundToInt(startPos.y * 1000)).ToString(); // (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000)).ToString();
        else
            if ((visCamera == MetricHolePoint.VisibleByCamEnum.Top) || (visCamera == MetricHolePoint.VisibleByCamEnum.Bot))
            someCubePointPosINformMeshText.GetComponent<TextMesh>().text = "y=" + (Mathf.RoundToInt(startPos.y * 1000)).ToString() + "\nz=" + (Mathf.RoundToInt(startPos.z * 1000)).ToString(); // (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000)).ToString();


        if (visCamera==MetricHolePoint.VisibleByCamEnum.Left)
            if (holeCubeNum==MetricHolePoint.HoleCubeNum.One)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;
            else
                if (holeCubeNum == MetricHolePoint.HoleCubeNum.Three)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleRight;


        if (visCamera == MetricHolePoint.VisibleByCamEnum.Right)
                if (holeCubeNum == MetricHolePoint.HoleCubeNum.One)
                    someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleRight;
                else
                    if (holeCubeNum == MetricHolePoint.HoleCubeNum.Three)
                    someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;


        if (visCamera == MetricHolePoint.VisibleByCamEnum.Bot)
            if (holeCubeNum == MetricHolePoint.HoleCubeNum.One)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;
            else
                if (holeCubeNum == MetricHolePoint.HoleCubeNum.Three)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleRight;


        if (visCamera == MetricHolePoint.VisibleByCamEnum.Top)
            if (holeCubeNum == MetricHolePoint.HoleCubeNum.One)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleRight;
            else
                if (holeCubeNum == MetricHolePoint.HoleCubeNum.Three)
                someCubePointPosINformMeshText.GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;



            
        
          







        // someML_DistanceMeshTextGO.GetComponent<TextMesh>().color = MetricLineTextColor;
        someCubePointPosINformMeshText.GetComponent<TextMesh>().characterSize = bs.MetricLineTextCharacterSize*0.4f;
       // someCubePointPosINformMeshText.GetComponent<Renderer>().materials[0].color= bs.MetricLineTextColor;
        someCubePointPosINformMeshText.GetComponent<TextMesh>().font = bs.MetricLinesDistanceTextFont;
        MeshRenderer meshRendererComponent = someCubePointPosINformMeshText.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        someCubePointPosINformMeshText.GetComponent<Renderer>().materials = new Material[1];
        // someML_DistanceMeshTextGO.GetComponent<Renderer>().materials[0] = MetricLinesDistanceTexttMaterial;
        someCubePointPosINformMeshText.GetComponent<Renderer>().material = bs.MetricLinesDistanceTexttMaterial;
        //someML_DistanceMeshTextGO.GetComponent<TextMesh>().anchor=TextAnchor. 
        someMetricPointPosInformGO.AddComponent<MeshFilter>();
        var rend = someMetricPointPosInformGO.AddComponent<MeshRenderer>();
        rend.material = bs.MetricLinesMaterial;


        MeshFilter meshFilter = (MeshFilter)someMetricPointPosInformGO.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        float length = bs.MetricLineWidth;

        float width = bs.MetricLineWidth;
        float height = Vector3.Distance(startPos, endPos) - cutMetricLinesAtEndAndStart * 2f;


        //var elementInfo = someGO.AddComponent<WallElementInfoScript>();
        //elementInfo.length = length;
        //elementInfo.height = width;



        #region Vertices
        Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);
        Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);
        Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);
        Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);

        Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);
        Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);
        Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);
        Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);

        Vector3[] vertices = new Vector3[]
        {
	// Bottom
	p0, p1, p2, p3,
 
	// Left
	p7, p4, p0, p3,
 
	// Front
	p4, p5, p1, p0,
 
	// Back
	p6, p7, p3, p2,
 
	// Right
	p5, p6, p2, p1,
 
	// Top
	p7, p6, p5, p4
        };
        #endregion

        #region Normales
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {
	// Bottom
	down, down, down, down,
 
	// Left
	left, left, left, left,
 
	// Front
	front, front, front, front,
 
	// Back
	back, back, back, back,
 
	// Right
	right, right, right, right,
 
	// Top
	up, up, up, up
        };
        #endregion

        #region UVs
        Vector2 _00 = new Vector2(0f, 0f);
        Vector2 _10 = new Vector2(1f, 0f);
        Vector2 _01 = new Vector2(0f, 1f);
        Vector2 _11 = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
        {
	// Bottom
	_11, _01, _00, _10,
 
	// Left
	_11, _01, _00, _10,
 
	// Front
	_11, _01, _00, _10,
 
	// Back
	_11, _01, _00, _10,
 
	// Right
	_11, _01, _00, _10,
 
	// Top
	_11, _01, _00, _10,
        };
        #endregion

        #region Triangles
        int[] triangles = new int[]
        {
	// Bottom
	3, 1, 0,
    3, 2, 1,			
 
	// Left
	3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
    3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
	// Front
	3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
    3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
	// Back
	3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
    3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
	// Right
	3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
    3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
	// Top
	3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
    3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

        };
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        ;


        someCubePointPosINformMeshText.transform.position = endPos;
        if (visCamera==MetricHolePoint.VisibleByCamEnum.Left)
        {
            someCubePointPosINformMeshText.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
            if (visCamera == MetricHolePoint.VisibleByCamEnum.Top)
            {
                someCubePointPosINformMeshText.transform.eulerAngles = new Vector3(0, 90, 0);
            }

        else
            if (visCamera == MetricHolePoint.VisibleByCamEnum.Right)
        {
            someCubePointPosINformMeshText.transform.eulerAngles = new Vector3(0, 90 + 90, 0);
        }
        if (visCamera == MetricHolePoint.VisibleByCamEnum.Bot)
        {
            someCubePointPosINformMeshText.transform.eulerAngles = new Vector3(0, 270, 0);
        }
        someMetricPointPosInformGO.transform.position = startPos;
        someMetricPointPosInformGO.transform.LookAt(endPos);
        GameObject MetricLineConeStart = GameObject.Instantiate(bs.MetricLinesCone);
        MetricLineConeStart.SetActive(true);
        MetricLineConeStart.transform.position = startPos;
        MetricLineConeStart.transform.LookAt(endPos);
        someMetricPointPosInformGO.transform.position = new Vector3((endPos.x + startPos.x) / 2, (endPos.y + startPos.y) / 2f, (endPos.z + startPos.z) / 2f);
        MetricLineConeStart.transform.parent = someMetricPointPosInformGO.transform;



        if (visCamera==MetricHolePoint.VisibleByCamEnum.Left)
        ParentPoint.transform.Translate(0, 0, -(bs.OffsetValueToDirectionPerspCam1+bs.TolschinaStenKarkasa/100f/2f), Space.Self);

        if (visCamera == MetricHolePoint.VisibleByCamEnum.Right)
            ParentPoint.transform.Translate(0, 0, (bs.OffsetValueToDirectionPerspCam1 + bs.TolschinaStenKarkasa / 100f / 2f), Space.Self);

        if (visCamera == MetricHolePoint.VisibleByCamEnum.Bot)
            ParentPoint.transform.Translate((bs.OffsetValueToDirectionPerspCam1 + bs.TolschinaStenKarkasa / 100f / 2f), 0, 0,  Space.Self);

        if (visCamera == MetricHolePoint.VisibleByCamEnum.Top)
            ParentPoint.transform.Translate(-(bs.OffsetValueToDirectionPerspCam1 + bs.TolschinaStenKarkasa / 100f / 2f), 0, 0, Space.Self);
    }


}



public class MetricLine
{
    public Vector3 LineStart;
    public Vector3 LineEnd;
    

    public enum TextSideEnum { Left, Right, Top, Bot};
    public TextSideEnum TextSkakoiStorony;

    public enum VisCamEnum { LeftCam, RightCam, TopCam, BotCam, UpCam };
    public VisCamEnum VisisbleByCam;
    public enum LineDirectionsEnum { x, y, z };
    public LineDirectionsEnum MetricLineDirection;
    public GameObject DistanceMeshTextGO;

    public enum ParentObjTypeEnum { Window, Door, Wall, Skam,  Undefined};
    public ParentObjTypeEnum ParentObjType;

    public  MetricLine(Vector3 linestart, Vector3 lineend,  TextSideEnum textSkakoiStorony, VisCamEnum visisbleByCam, ParentObjTypeEnum parentObjType)
    {
        LineStart = linestart;
        LineEnd = lineend;
        TextSkakoiStorony = textSkakoiStorony;

        VisisbleByCam = visisbleByCam;
        ParentObjType = parentObjType;

    }



    public MetricLine(Vector3 linestart, Vector3 lineend, TextSideEnum textSkakoiStorony, VisCamEnum visisbleByCam)
    {
        LineStart = linestart;
        LineEnd = lineend;
        TextSkakoiStorony = textSkakoiStorony;

        VisisbleByCam = visisbleByCam;
        ParentObjType = ParentObjTypeEnum.Undefined;

    }

}







public class BathGeneralInfoScript : MonoBehaviour {

    [Multiline]
    public string CodeOfBanya;
    public bool CodeReadedFromFile;
    public Material FloorAndWallWithoutTexturesMat;
    public List<MetricLine> MetricLines;
    

    public GameObject  UglovoiStoldTL;
    public GameObject  UglovoiStoldTR;
    public GameObject UglovoiStoldBL;
    public GameObject UglovoiStoldBR;

    public float WallsUV_ScaleMultiplier = 0.5f;
    public float ExternalWallsUV_XScaleMultiplier = 0.5f;
    public float ExternalWallsUV_YScaleMultiplier = 0.5f;

    public GameObject StenaDlina1;
    public GameObject StenaDlina2;
    public GameObject StenaShirina1_Parilka;
    public GameObject StenaShirina2_SDveryu;
    public GameObject Peregorodka1;
    public GameObject Peregorodka2;
    public GameObject StenkaDusha;
    public bool StenkaDushaIn2SectionBanyaTheSamePosAsStenaSDveryu;
    public string StenkaDushaRightOrLeft;
    public string DefaultOpenDoorDirection = "OutdoorLeft";
    public float HolesConturWidth = 0.03f;
    public float HolesConturDepth = 0.08f;
    public bool HideAllCubes;


    public float StdWindow_Height;
    public float StdWindow_Width;
    public float StdWindow_DistanceFromFloor;
    public float StdParilkaWindow_Height = 40;

    public float StdDoor_Height;
    public float StdDoor_Width;
    public float StdDoor_DistanceFromFloor;
    public float Std_OtdushinaLength;
    public float StdOtdushina_Width;
    public float StdOtdushina_DistanceFromTop;


    [Space]
    [Space]
    public float BanyaLength;
    public float BanyaWidth;
    public float BanyaHeight;
    public float ProemInSec2Height = 200f;
    public float XPosOfPeregorodkaParilnogo;
    public float DlinaMoechnoi;
    public float XPosOfPeregorodkaMoechnoi;


    public GameObject UIDushPosScheme_2sectionIndoor;
    public GameObject UIDushPosScheme_2sectionOutdoor;
    public GameObject UIDushPosScheme_3sectionPechIndoor;
    public GameObject UIDushPosScheme_3sectionPechOutdoor;





    public Dropdown UIDropdownForSectionCount;
    public Text UITextInfoForSectionCount;




    public InputField UIMoechnayaDlinaIF;
    public Text UIMoechnayaDlinaText;

    public float XposOfStenkaDusha;
    public float RasstOtParilkiToStenkaDusha;
    //public float 

    public InputField UIRasstOtParilkiToStenkaDushaIF;
    public Text UIRasstOtParilkiToStenkaDushaText;

    public enum BanyaLengthSetFromType { InitialValue, FromInputField, FromDropdownList, FromGizmoManip, FromImportedFile };
    public BanyaLengthSetFromType LastBanyaLengthSetFrom;
    public InputField BanayLengthIF, BanayWidthIF;
    public Text BanyaLengthInfo, BanyaWidthInfo;

    public float TolschinaStenKarkasa = 0.1f;
    public float TolschinaWoodObshivki = 0.001f;
    public InputField TolschinaStenKarkasaIF;
    public Dropdown StandartDropdown;





    [Space]
    [Space]
    public GameObject Pech;
    public float DistanceFromPechToPeregorodka = 0.3f;
    public Toggle TogglePechVisibility;

    [Space]
    public GameObject WorldGroundPlane;
    public Toggle ToggleWorldGroundPlaneVisibility;
    [Space]

    public Toggle ToggleLampsVisibility;
    [Space]
    public GameObject Sidushki;
    public Toggle ToggleSidushkiVisibility;
    [Space]
    public bool WallsTexturesVisible;
    public Toggle ToggleWallsTexturesVisibility;
    [Space]
    public GameObject[] Dveri;
    public Toggle ToggleDveriVisibility;
    [Space]
    public GameObject[] StolsAndSkams;

    public Toggle ToggleStolsAndSkamsVisibility;

    [Space]

    [Space]

    public Dropdown DropdownOptionForSidePolkaVParilke;
    public GameObject SidePolkaParilkaLeft, SidePolkaParilkaRight;
    public GameObject MaxiBokovyeSidushkiParent;

    public Dropdown ParilkaSchemes;

    public Dropdown DropdownMiniParilkaNiznyaPolka;


    public GameObject GO_Text_ifParilkaMini;
    public GameObject GO_Dropdown_ifParilkaMini;

    public GameObject GO_Text_ifParilkaMaxi;

    public GameObject GO_Dropdown_ifParilkaMaxi;


    public GameObject GO_Text_ifParilkaStandart;
    public GameObject GO_Dropdown_ifParilkaStandart;

    public GameObject MiniParilkaNizhnyaLeftPolka;
    public GameObject MiniParilkaNizhnyaRightPolka;
    public GameObject StandartNizhnyaPolka;

    public string PechRightOrLeft = "Right";
    public string PechIndoorOrOutdoor = "Indoor"; //
    public Toggle PechOutdoorToggle;


    public Dropdown DropdownPechRightOrLeft;


    GameObject[] AllWalls;

    public Toggle ToggleNalichieDusha;
    public GameObject UIDushSelectionPanel;
    public GameObject UIBtnSelectionDushPos;
    public GameObject Dush;
    public Transform DushTransfrom1;
    public Transform DushTransfrom2;
    public Transform DushTransfrom3;
    public Transform DushTransfrom4;

    public string DushNalichie;
    public string DushPos;
    public int SectionCount = 2;
    public Toggle ToggleDushPos1;
    public Toggle ToggleDushPos2;
    public Toggle ToggleDushPos3;
    public Toggle ToggleDushPos4;

    public GameObject BigStolGO;
    public Toggle ToggleNalichieBigStola;
    public string BigStolNalichie;

    public GameObject MalStolGO;
    public Toggle ToggleNalichieMalStola;
    public string MalStolNalichie;


    public GameObject UISelectSkameikiPosVMoechnoiPanel;
    public GameObject UIBtnSelectSkameikiPosVMoechnoi;
    public GameObject Image_3sectionPecnIndoorRightSkam;
    public GameObject Image_3sectionPecnIndoorLeftSkam;
    public GameObject Image_3sectionPechOutdoorSkamsObschaya;


    public Toggle ToggleSelectHorRightSkamNizOutdoorPech3Section;  //позиция 4 гори
    public Toggle ToggleSelectHorRightSkamVerhOutdoorPech3Section;  //3 гориз
    public Toggle ToggleSelectHorLeftSkamNizOutdoorPech3Section;   // 1 гориз
    public Toggle ToggleSelectHorLeftSkamVerhOutdoorPech3Section;  //2 гориз

    public Toggle ToggleSelectVerticalRightSkamNizOutdoorPech3Section;  //вертикал 4
    public Toggle ToggleSelectVerticalRightSkamVerhOutdoorPech3Section;  //вертикал 3
    public Toggle ToggleSelectVerticalLeftSkamNizOutdoorPech3Section;   //вертикал 1
    public Toggle ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section;   //вертикал 2

    public GameObject SmallSkam1;
    public GameObject SmallSkam2;
    public GameObject SmallSkam3;
    public GameObject SmallSkam4;


    public Transform PechOutDoorSkam1Transfrom;
    public Transform PechOutDoorSkam2Transfrom;
    public Transform PechOutDoorSkam3Transfrom;
    public Transform PechOutDoorSkam4Transfrom;
    public Transform PechInDoorSkam1Transfrom;
    public Transform PechInDoorSkam2Transfrom;
    public Transform PechInDoorSkam3Transfrom;
    public Transform PechInDoorSkam4Transfrom;
    public float DlinaMaloiSkam;
    public float ShirinaMaloiSkam;

    [Space]
    public Toggle ToggleSelectRightSkamIndoorPech3Section;
    public Toggle ToggleSelectLeftSkamIndoorPech3Section;
    public GameObject BigSkamLeft;
    public GameObject BigSkamRight;
    public float DlinaBigSkam;
    public float ShirinaBigSkam;


    [Space]
    public Dropdown DropdownSelectWallForDeleteAdd;

    public Toggle ToggleDeleteSelectHoleToWall;
    public Toggle ToggleHoleAsWindowAdd;
    public Toggle ToggleHoleAsDoorAdd;
    public Toggle ToggleHoleAsOtdushinaAdd;

    public GameObject WindowZGOToInstanciate;
    public GameObject WindowXGOToInstanciate;

    public GameObject OtdushinaZGOToInstanciate;
    public GameObject OtdusinaXGOToInstanciate;

    public GameObject DoorZGOToInstanciate;
    public GameObject DoorXGOToInstanciate;
    public GameObject ProemGOToInstanciate;
    [Space]


    public GameObject Slive1MidPoShirineParilka;
    public GameObject Slive2MidParilkaSleva;
    public GameObject Slive3MidParilkaSprava;
    public GameObject Slive4MidMoechnayaSleva;
    public GameObject Slive5MidMoechnayaSprava;

    public Toggle ToggleSlivePos1;
    public Toggle ToggleSlivePos2;
    public Toggle ToggleSlivePos3;
    public Toggle ToggleSlivePos4;
    public Toggle ToggleSlivePos5;
    public GameObject SlivPosSoglasovaniePanel;
    public GameObject ImageSchemeSlivPos3;
    public GameObject ImageSchemeSlivPos5;
    public GameObject _220PosSoglasovaniePanel;
    public Toggle Toggle220Pos1;
    public Toggle Toggle220Pos2;
    public Toggle Toggle220Pos3;
    public Toggle Toggle220Pos4;
    public Toggle Toggle220Pos5;
    public Toggle Toggle220Pos6;
    public Toggle Toggle220Pos7;
    public Toggle Toggle220Pos8;
    public GameObject _220vPos1;
    public GameObject _220vPos2;
    public GameObject _220vPos3;
    public GameObject _220vPos4;
    public GameObject _220vPos5;
    public GameObject _220vPos6;
    public GameObject _220vPos7;
    public GameObject _220vPos8;
    public int _220_OtstupSverhu;
    public int _220_OtstupSKrayu;


    public GameObject DushWaterPosSoglasovaniePanel;
    public Toggle ToggleDushWaterWall1;
    public Toggle ToggleDushWaterWall2;
    public Toggle ToggleDushWaterWall3;

    public GameObject DushWaterPos1;
    public GameObject DushWaterPos2;
    public GameObject DushWaterPos3;
    public Slider SliderDushWaterWallPercent;
    public GameObject BtnSoglasovatDushWater;
    public Toggle ToggleUglovLampLeftTopInParilka;
    public Toggle ToggleUglovLampRightTopInParilka;
    public Toggle ToggleUglovLampBottomInParilka;
    public Button BtnShowToggleOfLightsInParilka;
    public Button BtnHideToggleOfLightsInParilka;

    public GameObject UglovLampLeftTopInParilka, UglovLampRightTopInParilka, UglovLampBottomInParilka;

    public float UgolBottomLampXPosOffset;



    [Space]


    public string DoorOrProemInSec2;  //Door Proem
    public Toggle ToggleDoorInSec2;

    public HoleScript HoleAsDoorInPeregorodka2;
    public HoleScript HoleAsProemInPeregorodka2;
    public GameObject CanvasGO;
    string HoleInfoInPeregorodka2;
    [Space]

    public Toggle ToggleShowGrid;

    public float SkamsMetricLinesYOffset = 0.07f;
    public float ML_point_offset = 0.075f;
    public List<MetricHolePoint> MetricPointsEndStartPos;
    public List<GameObject> MetricHolePointGOs;
    [Space]
    public List<Vector3> MetricLinesEndStartPos;
    public List<GameObject> MetricLinesGO;
    public List<GameObject> MetricLinesEndCones;
    public List<GameObject> MetricLinesStartCones;
    //public List<GameObject> MetricLines_DistanceMeshTextGO;
    //public List<GameObject> ML_DistanceMeshTextGO;



    public GameObject MetricLinesCone;
    public Material MetricLinesMaterial;
    public float MetricLineWidth;
    public Color MetricLineTextColor;
    public float MetricLineTextCharacterSize;
    public bool ShowMetricLines;
    public Toggle ToggleShowMetricLines;
    [Space]
    public InputField LeftPrimechanie;
    public InputField RightPrimechanie;
    public InputField TopPrimechanie;
    public InputField BotPrimechanie;
    public InputField UpPrimechanie;
    public bool RezhimChertezhei;


    public float MetricLinesOffsetFromRealLiine;
    public Material MetricLinesDistanceTexttMaterial;
    public Font MetricLinesDistanceTextFont;
    public float OffsetValueToDirectionFromLines;
    public float OffsetValueToDirectionPerspCam1 = 0.06f;
    public float OffsetValueToDirectionLookingCameraRezhimChert = 0.06f;
    public float OffsetValueToDirectionUpCamera = 0.01f;
    
   
    
    

    [Space]
    public GameObject LightHelperTopWallSection2;
    public GameObject LightHelperBotWallSection2;
    public GameObject LightHelperRightWallSection2;
    public GameObject LightHelperLeftWallSection2;

    [Space]
    public GameObject LightHelperTopWallSection3;
    public GameObject LightHelperBotWallSection3;
    public GameObject LightHelperRightWallSection3;
    public GameObject LightHelperLeftWallSection3;
    [Space]


    public GameObject LightHelperSec2_IfDushNalFromParilkaToDush;
    public GameObject LightHelperSec2_IfDushNalFromDushToStenda2Shirina;
    public GameObject LightHelperSec2_IfDushNalBackStenkaDush;
    [Space]


    public Dropdown DropdownSelectionOfSectionForLightGeneration;
    [Space]



    [Space]
    [Space]


    public InputField IF_Sec2IfDushNalFromParilkaToDushLightsCount;  //если душ справа,  от парилки до душа
    public InputField IF_Sec2IfDushNalFromParilkaToDushTopLimit;
    public InputField IF_Sec2IfDushNalFromParilkaToDushBotLimit;


    [Space]
    public InputField IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount;   //если душ справ, от душа до стены ширина2
    public InputField IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit;
    public InputField IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit;

    [Space]


    public InputField IF_Sec2IfDushNalBackStenkaDushLightsCount;   //на обратной стороне стенки душа
    public InputField IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit;
    public InputField IF_Sec2IfDushNalBackStenkaDushLightsRightLimit;





    [Space]
    [Space]

    public InputField TopWallLightCountSec2;
    public InputField TopWallLightLeftLimitSec2;
    public InputField TopWallLightRightLimitSec2;
    [Space]
    public InputField BotWallLightCountSec2;
    public InputField BotWallLightLeftLimitSec2;
    public InputField BotWallLightRightLimitSec2;
    [Space]
    public InputField RightWallLightCountSec2;
    public InputField RightWallLightTopLimitSec2;
    public InputField RightWallLightBotLimitSec2;
    [Space]
    public InputField LeftWallLightCountSec2;
    public InputField LeftWallLightTopLimitSec2;
    public InputField LeftWallLightBotLimitSec2;


    [Space]
    [Space]
    public InputField TopWallLightCountSec3;
    public InputField TopWallLightLeftLimitSec3;
    public InputField TopWallLightRightLimitSec3;
    [Space]
    public InputField BotWallLightCountSec3;
    public InputField BotWallLightLeftLimitSec3;
    public InputField BotWallLightRightLimitSec3;
    [Space]
    public InputField RightWallLightCountSec3;
    public InputField RightWallLightTopLimitSec3;
    public InputField RightWallLightBotLimitSec3;
    [Space]
    public InputField LeftWallLightCountSec3;
    public InputField LeftWallLightTopLimitSec3;
    public InputField LeftWallLightBotLimitSec3;




    [Space]
    [Space]
    [Space]

    public GameObject PanelLightConfigInSection2_3;
    public float WidthOfSphericLight;
    public GameObject LampSphericToInstantiate;
    public GameObject LampBoundingBox;
    public GameObject JustCube;
    public List<GameObject> LampsOf2_3Sections;
    [Space]
    public List<GameObject> TopWallSection2Lamps;
    public List<GameObject> BotWallSection2Lamps;
    public List<GameObject> RightWallSection2Lamps;
    public List<GameObject> LeftWallSection2Lamps;
    [Space]

    public List<GameObject> Sec2IfDushNalFromParilkaToDushLights;
    public List<GameObject> Sec2IfDushNalFromDushToStenda2ShirinaLights;
    public List<GameObject> Sec2IfDushNalBackStenkaDushLights;

    [Space]
    public List<GameObject> TopWallSection3Lamps;
    public List<GameObject> BotWallSection3Lamps;
    public List<GameObject> RightWallSection3Lamps;
    public List<GameObject> LeftWallSection3Lamps;


    string


         Sec2IfDushNalFromParilkaToDushLightCount, Sec2IfDushNalFromParilkaToDushLightTopLimit, Sec2IfDushNalFromParilkaToDushLightBotLimit,
         Sec2IfDushNalFromDushToStenda2ShirinaLightCount, Sec2IfDushNalFromDushToStenda2ShirinaTopLimit, Sec2IfDushNalFromDushToStenda2ShirinaBotLimit,
         Sec2IfDushNalBackStenkaDushCount, Sec2IfDushNalBackStenkaDushLeftLimit, Sec2IfDushNalBackStenkaDushRightLimit,




        Sec2TopWallLightCount, Sec2TopWallLightLeftLimit, Sec2TopWallLightRightLimit,
          Sec2BotWallLightCount, Sec2BotWallLightLeftLimit, Sec2BotWallLightRightLimit,
          Sec2RightWallLightCount, Sec2RightWallLightTopLimit, Sec2RightWallLightBotLimit,
          Sec2LeftWallLightCount, Sec2LeftWallLightTopLimit, Sec2LeftWallLightBotLimit,


         Sec3TopWallLightCount, Sec3TopWallLightLeftLimit, Sec3TopWallLightRightLimit,
          Sec3BotWallLightCount, Sec3BotWallLightLeftLimit, Sec3BotWallLightRightLimit,
          Sec3RightWallLightCount, Sec3RightWallLightTopLimit, Sec3RightWallLightBotLimit,
          Sec3LeftWallLightCount, Sec3LeftWallLightTopLimit, Sec3LeftWallLightBotLimit;



    public List<HoleScript> DoorsInStenaDlina1;
    public List<HoleScript> DoorsInStenaDlina2;
    public List<HoleScript> DoorsInStenaShirina2_SDveryu;

    public List<HoleScript> DoorsInStenaShirina1_Parilka;
    public List<HoleScript> DoorsInPeregorodka1;
    public List<HoleScript> DoorsInPeregorodka2;

    [Space]


    public List<string> Doors_DirectionsInStenaDlina1;
    public List<string> Doors_DirectionsInStenaDlina2;
    public List<string> Doors_DirectionsInStenaShirina2_SDveryu;

    public List<string> Doors_DirectionsInStenaShirina1_Parilka;
    public List<string> Doors_DirectionsInPeregorodka1;
    public List<string> Doors_DirectionsInPeregorodka2;


    public Material FloorMaterial;
    public float FloorYpos = 0.0f;
    public GameObject FloorGO;


    public Button BtnLightCinfigPanelOpener;






    [Space]

    bool SkamTogglesActive;
    public GameObject SkamsConfigBtnsRotator;

    public GameObject TopSkam1GO;
    public GameObject TopSkam2GO;

    public GameObject BotSkam1GO;
    public GameObject BotSkam2GO;

    public GameObject RightSkam1GO;
    public GameObject RightSkam2GO;

    public GameObject LeftSkam1GO;
    public GameObject LeftSkam2GO;

    [Space]

    public GameObject[] SkamsGO;


    public Transform SkamTop_ToggleTarget;
    public Transform SkamBot_ToggleTarget;
    public Transform SkamRight_ToggleTarget;
    public Transform SkamLeft_ToggleTarget;

    [Space]
    public Toggle TopSkam1;
    public Toggle TopSkam2;
    [Space]
    public Toggle BotSkam1;
    public Toggle BotSkam2;
    [Space]
    public Toggle RightSkam1;
    public Toggle RightSkam2;
    [Space]
    public Toggle LeftSkam1;
    public Toggle LeftSkam2;


    public Toggle[] _2SectionSkamToggles;
    public GameObject BtnShowHideSkamToggles;
    public GameObject PanelSkamConfig;
    GameObject CurrentSkamInConfigPanel;
    public InputField skamDiscrementIF;







    public void SetOptionForBigStolRotation()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolRotX>", "</BigStolRotX>"), "<BigStolRotX>" + BigStolGO.transform.eulerAngles.x + "</BigStolRotX>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolRotY>", "</BigStolRotY>"), "<BigStolRotY>" + BigStolGO.transform.eulerAngles.y + "</BigStolRotY>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolRotZ>", "</BigStolRotZ>"), "<BigStolRotZ>" + BigStolGO.transform.eulerAngles.z + "</BigStolRotZ>");
        

    }


    public void ManageOptionForBigStolRotation(string BigStolRotX, string BigStolRotY, string BigStolRotZ)
    {
        BigStolGO.transform.eulerAngles = new Vector3(float.Parse(BigStolRotX), float.Parse(BigStolRotY), float.Parse(BigStolRotZ));

    }


    public void SetOptionForMalStolRotation()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolRotX>", "</MalStolRotX>"), "<MalStolRotX>" + MalStolGO.transform.eulerAngles.x + "</MalStolRotX>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolRotY>", "</MalStolRotY>"), "<MalStolRotY>" + MalStolGO.transform.eulerAngles.y + "</MalStolRotY>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolRotZ>", "</MalStolRotZ>"), "<MalStolRotZ>" + MalStolGO.transform.eulerAngles.z + "</MalStolRotZ>");
    }

    public void ManageOptionForMalStolRotation(string MalStolRotX, string MalStolRotY, string MalStolRotZ)
    {
        MalStolGO.transform.eulerAngles = new Vector3(float.Parse(MalStolRotX), float.Parse(MalStolRotY), float.Parse(MalStolRotZ));

    }







    public void PosUglovStolbs()
    {
        if (ToggleWallsTexturesVisibility.isOn)

        {

            UglovoiStoldTR.SetActive(true);
            UglovoiStoldTL.SetActive(true);
            UglovoiStoldBR.SetActive(true);
            UglovoiStoldBL.SetActive(true);

            UglovoiStoldTR.transform.position = new Vector3(0.5f * TolschinaStenKarkasa, -0.001f + BanyaHeight / 100f / 2f, BanyaWidth / 100f - 0.5f * TolschinaStenKarkasa);
            UglovoiStoldTL.transform.position = new Vector3(0.5f * TolschinaStenKarkasa, -0.001f + BanyaHeight / 100f / 2f, 0.5f * TolschinaStenKarkasa);
            UglovoiStoldBR.transform.position = new Vector3(BanyaLength / 100f - 0.5f * TolschinaStenKarkasa, -0.001f + BanyaHeight / 100f / 2f, BanyaWidth / 100f - 0.5f * TolschinaStenKarkasa);
            UglovoiStoldBL.transform.position = new Vector3(BanyaLength / 100f - 0.5f * TolschinaStenKarkasa, -0.001f + BanyaHeight / 100f / 2f, 0.5f * TolschinaStenKarkasa);
            UglovoiStoldTR.transform.localScale = new Vector3(0.101f, BanyaHeight / 100f, 0.101f);
            UglovoiStoldTL.transform.localScale = new Vector3(0.101f, BanyaHeight / 100f, 0.101f);
            UglovoiStoldBR.transform.localScale = new Vector3(0.101f, BanyaHeight / 100f, 0.101f);
            UglovoiStoldBL.transform.localScale = new Vector3(0.101f, BanyaHeight / 100f, 0.101f);
        }
        else
        {
            UglovoiStoldTR.SetActive(false);
            UglovoiStoldTL.SetActive(false);
            UglovoiStoldBR.SetActive(false);
            UglovoiStoldBL.SetActive(false);


        }
    }



    public void SetOptionForBigStolPosition()
    {      
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolPosX>", "</BigStolPosX>"), "<BigStolPosX>" + BigStolGO.transform.position.x + "</BigStolPosX>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolPosY>", "</BigStolPosY>"), "<BigStolPosY>" + BigStolGO.transform.position.y + "</BigStolPosY>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolPosZ>", "</BigStolPosZ>"), "<BigStolPosZ>" + BigStolGO.transform.position.z + "</BigStolPosZ>");
    }
    public void ManageOptionForBigStolPosition(string BigStolPosX, string BigStolPosY, string BigStolPosZ)
    {   BigStolGO.transform.position = new Vector3(float.Parse(BigStolPosX), float.Parse(BigStolPosY), float.Parse(BigStolPosZ));
        
    }




    public void SetOptionForMalStolPosition()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolPosX>", "</MalStolPosX>"), "<MalStolPosX>" + MalStolGO.transform.position.x + "</MalStolPosX>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolPosY>", "</MalStolPosY>"), "<MalStolPosY>" + MalStolGO.transform.position.y + "</MalStolPosY>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolPosZ>", "</MalStolPosZ>"), "<MalStolPosZ>" + MalStolGO.transform.position.z + "</MalStolPosZ>");
    }
    public void ManageOptionForMalStolPosition(string MalStolPosX, string MalStolPosY, string MalStolPosZ)
    {
        MalStolGO.transform.position = new Vector3(float.Parse(MalStolPosX), float.Parse(MalStolPosY), float.Parse(MalStolPosZ));

    }











    #region skams




    public void ShowSkamPanel(GameObject someSkam)
    {

        PanelSkamConfig.SetActive(true);
        CurrentSkamInConfigPanel = someSkam;



        if ((someSkam==TopSkam1GO)|| (someSkam == TopSkam2GO)|| (someSkam == BotSkam1GO)|| (someSkam == BotSkam2GO))
        {
            SkamsConfigBtnsRotator.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
        }
        else
          if ((someSkam == LeftSkam1GO) || (someSkam == LeftSkam2GO) || (someSkam == RightSkam1GO) || (someSkam == RightSkam2GO))
        {
            SkamsConfigBtnsRotator.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -90);
        }




    }




    public void  SetOptionsForAllSkams()
    {
        SetOptionForTopSkam1_MinMax();
        SetOptionForTopSkam2_MinMax();

        SetOptionForBotSkam1_MinMax();
        SetOptionForBotSkam2_MinMax();

        SetOptionForLeftSkam1_MinMax();
        SetOptionForLeftSkam2_MinMax();

        SetOptionForRightSkam1_MinMax();
        SetOptionForRightSkam2_MinMax();
    }

    public void SetOptionForTopSkam1_MinMax()
    {      CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam1_Min>", "</TopSkam1_Min>"), "<TopSkam1_Min>" + TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin + "</TopSkam1_Min>");
           CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam1_Max>", "</TopSkam1_Max>"), "<TopSkam1_Max>" + TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax + "</TopSkam1_Max>");
    }
    public void ManageOptionForTopSkam1_MinMax(string min, string max)
    {   TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin = float.Parse(min);
        TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax = float.Parse(max);
        TopSkam1GO.GetComponent<SkamScript>().CalculateCalues();
    }




    public void SetOptionForTopSkam2_MinMax()
    {   CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam2_Min>", "</TopSkam2_Min>"), "<TopSkam2_Min>" + TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin + "</TopSkam2_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam2_Max>", "</TopSkam2_Max>"), "<TopSkam2_Max>" + TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax + "</TopSkam2_Max>");
    }
    public void ManageOptionForTopSkam2_MinMax(string min, string max)
    {
        TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin = float.Parse(min);
        TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax = float.Parse(max);
        TopSkam2GO.GetComponent<SkamScript>().CalculateCalues();
    }





    public void SetOptionForBotSkam1_MinMax()
    {   CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam1_Min>", "</BotSkam1_Min>"), "<BotSkam1_Min>" + BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin + "</BotSkam1_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam1_Max>", "</BotSkam1_Max>"), "<BotSkam1_Max>" + BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax + "</BotSkam1_Max>");
    }
    public void ManageOptionForBotSkam1_MinMax(string min, string max)
    {
        BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin = float.Parse(min);
        BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax = float.Parse(max);
        BotSkam1GO.GetComponent<SkamScript>().CalculateCalues();
    }

    

    public void SetOptionForBotSkam2_MinMax()
    {   CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam2_Min>", "</BotSkam2_Min>"), "<BotSkam2_Min>" + BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin + "</BotSkam2_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam2_Max>", "</BotSkam2_Max>"), "<BotSkam2_Max>" + BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax + "</BotSkam2_Max>");
    }
    public void ManageOptionForBotSkam2_MinMax(string min, string max)
    {
        BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin = float.Parse(min);
        BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax = float.Parse(max);
        BotSkam2GO.GetComponent<SkamScript>().CalculateCalues();
    }







    public void SetOptionForLeftSkam1_MinMax()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam1_Min>", "</LeftSkam1_Min>"), "<LeftSkam1_Min>" +LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin + "</LeftSkam1_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam1_Max>", "</LeftSkam1_Max>"), "<LeftSkam1_Max>" + LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax + "</LeftSkam1_Max>");
    }
    public void ManageOptionForLeftSkam1_MinMax(string min, string max)
    {
        LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin = float.Parse(min);
        LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax = float.Parse(max);
        LeftSkam1GO.GetComponent<SkamScript>().CalculateCalues();
    }




    public void SetOptionForLeftSkam2_MinMax()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam2_Min>", "</LeftSkam2_Min>"), "<LeftSkam2_Min>" + LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin + "</LeftSkam2_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam2_Max>", "</LeftSkam2_Max>"), "<LeftSkam2_Max>" + LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax + "</LeftSkam2_Max>");
    }
    public void ManageOptionForLeftSkam2_MinMax(string min, string max)
    {
        LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin = float.Parse(min);
        LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax = float.Parse(max);
        LeftSkam2GO.GetComponent<SkamScript>().CalculateCalues();
    }



    public void SetOptionForRightSkam1_MinMax()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam1_Min>", "</RightSkam1_Min>"), "<RightSkam1_Min>" + RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin + "</RightSkam1_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam1_Max>", "</RightSkam1_Max>"), "<RightSkam1_Max>" + RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax + "</RightSkam1_Max>");
    }
    public void ManageOptionForRightSkam1_MinMax(string min, string max)
    {
        RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin = float.Parse(min);
        RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax = float.Parse(max);
        RightSkam1GO.GetComponent<SkamScript>().CalculateCalues();
    }


    public void SetOptionForRightSkam2_MinMax()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam2_Min>", "</RightSkam2_Min>"), "<RightSkam2_Min>" + RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin + "</RightSkam2_Min>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam2_Max>", "</RightSkam2_Max>"), "<RightSkam2_Max>" + RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax + "</RightSkam2_Max>");
    }
    public void ManageOptionForRightSkam2_MinMax(string min, string max)
    {
        RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin = float.Parse(min);
        RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax = float.Parse(max);
        RightSkam2GO.GetComponent<SkamScript>().CalculateCalues();
    }




    public void SkamMoveBigger()
    {
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax + float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax + float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin + float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin + float.Parse(skamDiscrementIF.text) / 100f;
        SetOptionsForAllSkams();
    }


    public void SkamMoveSmaller()
    {
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax - float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax - float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin - float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin - float.Parse(skamDiscrementIF.text) / 100f;
        SetOptionsForAllSkams();

    }


    public void SkamMaxBigger()
    {    
            CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax + float.Parse(skamDiscrementIF.text)/100f;
            CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax + float.Parse(skamDiscrementIF.text)/100f;

        SetOptionsForAllSkams();
        //foreach (GameObject go in  SkamsGO)
        //{
        //    go.GetComponent<SkamScript>().CalculateCalues();

        //}

    }

    public void SkamMaxSmaller()
    {
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMax - float.Parse(skamDiscrementIF.text)/100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMax - float.Parse(skamDiscrementIF.text) / 100f;


        SetOptionsForAllSkams();

        //foreach (GameObject go in SkamsGO)
        //{

        //    go.GetComponent<SkamScript>().CalculateCalues();
        //}
    }

    public void SkamMinBigger()
    {
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin + float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin + float.Parse(skamDiscrementIF.text) / 100f;


        SetOptionsForAllSkams();

        //foreach (GameObject go in SkamsGO)
        //{

        //    go.GetComponent<SkamScript>().CalculateCalues();
        //}
    }

    public void SkamMinSmaller()
    {
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_X_LimitMin - float.Parse(skamDiscrementIF.text) / 100f;
        CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin = CurrentSkamInConfigPanel.GetComponent<SkamScript>().Sid_Z_LimitMin - float.Parse(skamDiscrementIF.text) / 100f;


        SetOptionsForAllSkams();

        //foreach (GameObject go in SkamsGO)
        //{

        //    go.GetComponent<SkamScript>().CalculateCalues();
        //}
    }














    public void HideSkamPanel()
    {

        PanelSkamConfig.SetActive(false);

    }






    public void ManageOptionForTopSkam1(string option)
    {
        if (option == "Enabled")
        {
            TopSkam1GO.SetActive(true);

            TopSkam1GO.GetComponent<SkamScript>().CalculateCalues();
            TopSkam1GO.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f +TolschinaStenKarkasa*2f, TopSkam1GO.transform.position.y, TopSkam1GO.transform.position.z);
            TopSkam1.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            TopSkam1GO.SetActive(false);
            TopSkam1.isOn = false;
        }
        
    }

    public void SetOptionForTopSkam1()
    {

        if (TopSkam1.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam1>", "</TopSkam1>"), "<TopSkam1>" + "Enabled" + "</TopSkam1>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam1>", "</TopSkam1>"), "<TopSkam1>" + "Disabled" + "</TopSkam1>");
        }

    }




    public void ManageOptionForTopSkam2(string option)
    {
        if (option == "Enabled")
        {
            TopSkam2GO.SetActive(true);
            TopSkam2GO.GetComponent<SkamScript>().CalculateCalues();
            TopSkam2.isOn = true;
            TopSkam2GO.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f + TolschinaStenKarkasa * 2f, TopSkam2GO.transform.position.y, TopSkam2GO.transform.position.z);
        }
        else
            if (option == "Disabled")
        {
            TopSkam2GO.SetActive(false);
            TopSkam2.isOn = false;
        }

    }
    public void SetOptionForTopSkam2()
    {

        if (TopSkam2.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam2>", "</TopSkam2>"), "<TopSkam2>" + "Enabled" + "</TopSkam2>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopSkam2>", "</TopSkam2>"), "<TopSkam2>" + "Disabled" + "</TopSkam2>");
        }

    }




    public void ManageOptionForBotSkam1(string option)
    {
        if (option == "Enabled")
        {
            BotSkam1GO.SetActive(true);
            BotSkam1GO.GetComponent<SkamScript>().CalculateCalues();
            
            BotSkam1GO.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f+TolschinaStenKarkasa * 2f, BotSkam1GO.transform.position.y, BotSkam1GO.transform.position.z);
            BotSkam1.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            BotSkam1GO.SetActive(false);
            BotSkam1.isOn = false;
        }

    }


    public void SetOptionForBotSkam1()
    {

        if (BotSkam1.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam1>", "</BotSkam1>"), "<BotSkam1>" + "Enabled" + "</BotSkam1>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam1>", "</BotSkam1>"), "<BotSkam1>" + "Disabled" + "</BotSkam1>");
        }

    }






    public void ManageOptionForBotSkam2(string option)
    {
        if (option == "Enabled")
        {
            BotSkam2GO.SetActive(true);
            BotSkam2GO.GetComponent<SkamScript>().CalculateCalues();
            BotSkam2GO.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f+ TolschinaStenKarkasa * 2f, BotSkam2GO.transform.position.y, BotSkam2GO.transform.position.z);
            BotSkam2.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            BotSkam2GO.SetActive(false);
            BotSkam2.isOn = false;
        }

    }

    public void SetOptionForBotSkam2()
    {

        if (BotSkam2.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam2>", "</BotSkam2>"), "<BotSkam2>" + "Enabled" + "</BotSkam2>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotSkam2>", "</BotSkam2>"), "<BotSkam2>" + "Disabled" + "</BotSkam2>");
        }

    }






    public void ManageOptionForRightSkam1(string option)
    {
        if (option == "Enabled")
        {
           RightSkam1GO.SetActive(true);
            RightSkam1GO.GetComponent<SkamScript>().CalculateCalues();
            RightSkam1.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            RightSkam1GO.SetActive(false);
            RightSkam1.isOn = false;
        }

    }


    public void SetOptionForRightSkam1()
    {

        if (RightSkam1.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam1>", "</RightSkam1>"), "<RightSkam1>" + "Enabled" + "</RightSkam1>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam1>", "</RightSkam1>"), "<RightSkam1>" + "Disabled" + "</RightSkam1>");
        }

    }





    public void ManageOptionForRightSkam2 (string option)
    {
        if (option == "Enabled")
        {
            RightSkam2GO.SetActive(true);
            RightSkam2GO.GetComponent<SkamScript>().CalculateCalues();
            RightSkam2.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            RightSkam2GO.SetActive(false);
            RightSkam2.isOn = false;
        }

    }
    public void SetOptionForRightSkam2()
    {

        if (RightSkam2.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam2>", "</RightSkam2>"), "<RightSkam2>" + "Enabled" + "</RightSkam2>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightSkam2>", "</RightSkam2>"), "<RightSkam2>" + "Disabled" + "</RightSkam2>");
        }

    }





    public void ManageOptionForLeftSkam1(string option)
    {
        if (option == "Enabled")
        {
            LeftSkam1GO.SetActive(true);
            LeftSkam1GO.GetComponent<SkamScript>().CalculateCalues();
            LeftSkam1.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            LeftSkam1GO.SetActive(false);
            LeftSkam1.isOn = false;
        }

    }
    public void SetOptionForLeftSkam1()
    {

        if (LeftSkam1.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam1>", "</LeftSkam1>"), "<LeftSkam1>" + "Enabled" + "</LeftSkam1>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam1>", "</LeftSkam1>"), "<LeftSkam1>" + "Disabled" + "</LeftSkam1>");
        }

    }





    public void ManageOptionForLeftSkam2(string option)
    {
        if (option == "Enabled")
        {
            LeftSkam2GO.SetActive(true);
            LeftSkam2GO.GetComponent<SkamScript>().CalculateCalues();
            LeftSkam2.isOn = true;
        }
        else
            if (option == "Disabled")
        {
            LeftSkam2GO.SetActive(false);
            LeftSkam2.isOn = false;
        }

    }
    public void SetOptionForLeftSkam2()
    {

        if (LeftSkam2.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam2>", "</LeftSkam2>"), "<LeftSkam2>" + "Enabled" + "</LeftSkam2>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftSkam2>", "</LeftSkam2>"), "<LeftSkam2>" + "Disabled" + "</LeftSkam2>");
        }

    }





    #endregion
    public void SetPosForSkam_ToggleTargets()
    {
      SkamTop_ToggleTarget.position=new Vector3(XPosOfPeregorodkaParilnogo/100f, 0.5f, BanyaWidth/100f/2f);
      SkamBot_ToggleTarget.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f, 0.5f, BanyaWidth / 100f / 2f);
      SkamRight_ToggleTarget.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f+1f, 0.5f, BanyaWidth / 100f);
        SkamLeft_ToggleTarget.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f + 1f, 0.5f, 0f);
    }
        


    public void UI_ShowOrHideToggleForSkams()
    {
        if (_2SectionSkamToggles[0].gameObject.activeSelf)
        {
            BtnShowHideSkamToggles.GetComponent<Button>().GetComponentInChildren<Text>().text = "Скамьи";
            foreach (Toggle tg in _2SectionSkamToggles)
            {
                tg.gameObject.SetActive(false);
            }

            SkamTogglesActive = false;       
        }
        else
        {
            BtnShowHideSkamToggles.GetComponent<Button>().GetComponentInChildren<Text>().text = "OK";
            foreach (Toggle tg in _2SectionSkamToggles)
                {
                    tg.gameObject.SetActive(true);
                }

            if (SectionCount==2)
            {
                BotSkam1.gameObject.SetActive(false);
                BotSkam2.gameObject.SetActive(false);

            }

            SkamTogglesActive = true;
        }

      }







    public void UI_ShowOrHideToggleForBotSkamsWhenSkamToggleActive()
    {
        print("SkamTogglesActive" + SkamTogglesActive);
        if (SkamTogglesActive)
        {
            
            if (UIDropdownForSectionCount.value==1)
            {
                BotSkam1.gameObject.SetActive(true);
                BotSkam2.gameObject.SetActive(true);
            }
        }




        

    }








    public void HideBotSkamsIf2Section()
    {
        if (  UIDropdownForSectionCount.value ==0)
        {
            BotSkam1GO.SetActive(false);
            BotSkam2GO.SetActive(false);
            BotSkam1.isOn = false;
            BotSkam2.isOn = false;

            //SetOptionForBotSkam1();
            //SetOptionForBotSkam2();
        }
    }


    public void UI_HideToggleForSkams()
    {
    


            
    }








    //public void ManageOptionForHolesInWall1(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos)
    //{
    //    for (int i = 0; i < HolesCount; i++)
    //    {
    //        StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = Holes_Cube1Pos[i];
    //        StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube3Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
    //        StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = Holes_Cube3Pos[i];
    //        StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);
    //    }
    //}


    //public void SetOptionForHolesInWall1(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos)
    //{
    //    string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
    //    for (int i = 0; i < HolesCount; i++)
    //    {
    //        holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
    //    }


    //    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + holesDescription + "</Wall1Holes>");
    //}



    public void SetLeftPrimechanieOoption()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LeftPrimechanie>", "</LeftPrimechanie>"), "<LeftPrimechanie>" + LeftPrimechanie.text + "</LeftPrimechanie>");
    }
    public void Manage_LeftPrimechanieOoption(string option)
    {
        LeftPrimechanie.text = option;
    }


    public void SetRightPrimechanieOoption()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RightPrimechanie>", "</RightPrimechanie>"), "<RightPrimechanie>" + RightPrimechanie.text + "</RightPrimechanie>");
    }
    public void Manage_RightPrimechanieOoption(string option)
    {
        RightPrimechanie.text = option;
    }


    public void SetTopPrimechanieOoption()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TopPrimechanie>", "</TopPrimechanie>"), "<TopPrimechanie>" + TopPrimechanie.text + "</TopPrimechanie>");
    }
    public void Manage_TopPrimechanieOoption(string option)
    {
        TopPrimechanie.text = option;
    }


    public void SetBotPrimechanieOoption()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BotPrimechanie>", "</BotPrimechanie>"), "<BotPrimechanie>" + BotPrimechanie.text + "</BotPrimechanie>");
    }
    public void Manage_BotPrimechanieOoption(string option)
    {
        BotPrimechanie.text = option;
    }


    public void SetUpPrimechanieOoption()
    {
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UpPrimechanie>", "</UpPrimechanie>"), "<UpPrimechanie>" + UpPrimechanie.text + "</UpPrimechanie>");
    }
    public void Manage_UpPrimechanieOoption(string option)
    {
        UpPrimechanie.text = option;
    }



    public void BuildFloorXZ(float startX, float endX, float startZ, float endZ, float _depth)  //пол
    {
        if (FloorGO != null)
        {
            GameObject.Destroy(FloorGO, 0.01f);
        }
        FloorGO = new GameObject();
        FloorGO.name = "Floor";
        //someGO.transform.parent = this.transform;
        FloorGO.AddComponent<MeshFilter>();
        var rend = FloorGO.AddComponent<MeshRenderer>();
        if (WallsTexturesVisible)
        { rend.material = FloorMaterial; }
        else
            rend.material = FloorAndWallWithoutTexturesMat;


        MeshFilter meshFilter = (MeshFilter)FloorGO.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        float length = Mathf.Abs(endX - startX);    //Vector3.Distance(pos1, pos2) * 10 + HolesConturWidth * 10;

        float width = _depth;
        float height = Mathf.Abs(endZ - startZ); ;









        #region Vertices
        Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);
        Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);
        Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);
        Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);

        Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);
        Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);
        Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);
        Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);

        Vector3[] vertices = new Vector3[]
        {
	// Bottom
	p0, p1, p2, p3,
 
	// Left
	p7, p4, p0, p3,
 
	// Front
	p4, p5, p1, p0,
 
	// Back
	p6, p7, p3, p2,
 
	// Right
	p5, p6, p2, p1,
 
	// Top
	p7, p6, p5, p4
        };
        #endregion

        #region Normales
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {
	// Bottom
	down, down, down, down,
 
	// Left
	left, left, left, left,
 
	// Front
	front, front, front, front,
 
	// Back
	back, back, back, back,
 
	// Right
	right, right, right, right,
 
	// Top
	up, up, up, up
        };
        #endregion

        #region UVs
        Vector2 _00 = new Vector2(0f, 0f);
        Vector2 _10 = new Vector2(1f, 0f);
        Vector2 _01 = new Vector2(0f, 1f);
        Vector2 _11 = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
        {
	// Bottom
	_11, _01, _00, _10,
 
	// Left
	_11, _01, _00, _10,
 
	// Front
	_11, _01, _00, _10,
 
	// Back
	_11, _01, _00, _10,
 
	// Right
	_11, _01, _00, _10,
 
	// Top
	_11, _01, _00, _10,
        };
        #endregion

        #region Triangles
        int[] triangles = new int[]
        {
	// Bottom
	3, 1, 0,
    3, 2, 1,			
 
	// Left
	3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
    3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
	// Front
	3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
    3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
	// Back
	3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
    3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
	// Right
	3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
    3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
	// Top
	3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
    3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

        };
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        ;


        FloorGO.transform.position = new Vector3((endX + startX) / 2, FloorYpos, (endZ + startZ) / 2);






    }

    #region section2

    public void SetLightOpt_Sec2IfDushNalFromParilkaToDush()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsCount>", "</Sec2IfDushNalFromParilkaToDushLightsCount>"), "<Sec2IfDushNalFromParilkaToDushLightsCount>" + IF_Sec2IfDushNalFromParilkaToDushLightsCount.text + "</Sec2IfDushNalFromParilkaToDushLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsTopLimit>", "</Sec2IfDushNalFromParilkaToDushLightsTopLimit>"), "<Sec2IfDushNalFromParilkaToDushLightsTopLimit>" + IF_Sec2IfDushNalFromParilkaToDushTopLimit.text + "</Sec2IfDushNalFromParilkaToDushLightsTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsBotLimit>", "</Sec2IfDushNalFromParilkaToDushLightsBotLimit>"), "<Sec2IfDushNalFromParilkaToDushLightsBotLimit>" + IF_Sec2IfDushNalFromParilkaToDushBotLimit.text + "</Sec2IfDushNalFromParilkaToDushLightsBotLimit>");
    }
    public void ManageLightOpt_Sec2IfDushNalFromParilkaToDush(string lightCount, string topLimit, string botLimit)
    {
        CalculateLightHelpersPositions();
        IF_Sec2IfDushNalFromParilkaToDushLightsCount.text = lightCount;
        IF_Sec2IfDushNalFromParilkaToDushTopLimit.text = topLimit;
        IF_Sec2IfDushNalFromParilkaToDushBotLimit.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < Sec2IfDushNalFromParilkaToDushLights.Count; i++)
        { Destroy(Sec2IfDushNalFromParilkaToDushLights[i], 0.001f); }
        Sec2IfDushNalFromParilkaToDushLights.Clear();


        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.x + float.Parse(topLimit) / 100f * LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.x - LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.x + LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.x / 2;

            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject someLamp = GameObject.Instantiate(LampSphericToInstantiate);
                Sec2IfDushNalFromParilkaToDushLights.Add(someLamp);
                someLamp.SetActive(true);

                if (lightCount != "1")
                    someLamp.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.y, LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.z - TolschinaStenKarkasa / 2);
                else
                    someLamp.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.y, LightHelperSec2_IfDushNalFromParilkaToDush.transform.position.z - TolschinaStenKarkasa / 2);
                someLamp.transform.eulerAngles = new Vector3(0, -270, 0);
            }
        }

    }

    public void SetLightOpt_Sec2IfDushNalFromDushToStenda2Shirina()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>" + IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.text + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>" + IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.text + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>" + IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.text + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>");
    }
    public void ManageLightOpt_Sec2IfDushNalFromDushToStenda2Shirina(string lightCount, string topLimit, string botLimit)
    {
        CalculateLightHelpersPositions();


        IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.text = lightCount;
        IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.text = topLimit;
        IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < Sec2IfDushNalFromDushToStenda2ShirinaLights.Count; i++)
        { Destroy(Sec2IfDushNalFromDushToStenda2ShirinaLights[i], 0.001f); }
        Sec2IfDushNalFromDushToStenda2ShirinaLights.Clear();


        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.x + float.Parse(topLimit) / 100f * LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.x - LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.x + LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.x / 2;

            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject SomeLamp = GameObject.Instantiate(LampSphericToInstantiate);
                Sec2IfDushNalFromDushToStenda2ShirinaLights.Add(SomeLamp);
                SomeLamp.SetActive(true);
                if (lightCount != "1")
                    SomeLamp.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.y, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.z - TolschinaStenKarkasa / 2);
                else
                    SomeLamp.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.y, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position.z - TolschinaStenKarkasa / 2);
                SomeLamp.transform.eulerAngles = new Vector3(0, -270, 0);
            }
        }

    }


    public void SetLightOpt_Sec2IfDushNalBackStenkaDush()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsCount>", "</Sec2IfDushNalBackStenkaDushLightsCount>"), "<Sec2IfDushNalBackStenkaDushLightsCount>" + IF_Sec2IfDushNalBackStenkaDushLightsCount.text + "</Sec2IfDushNalBackStenkaDushLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsLeftLimit>", "</Sec2IfDushNalBackStenkaDushLightsLeftLimit>"), "<Sec2IfDushNalBackStenkaDushLightsLeftLimit>" + IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.text + "</Sec2IfDushNalBackStenkaDushLightsLeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsRightLimit>", "</Sec2IfDushNalBackStenkaDushLightsRightLimit>"), "<Sec2IfDushNalBackStenkaDushLightsRightLimit>" + IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.text + "</Sec2IfDushNalBackStenkaDushLightsRightLimit>");
    }
    public void ManageLightOpt_Sec2IfDushNalBackStenkaDush(string lightCount, string leftLimit, string rightLimit)
    {
        CalculateLightHelpersPositions();

        IF_Sec2IfDushNalBackStenkaDushLightsCount.text = lightCount;
        IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.text = leftLimit;
        IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.text = rightLimit;
        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < Sec2IfDushNalBackStenkaDushLights.Count; i++)
        { Destroy(Sec2IfDushNalBackStenkaDushLights[i], 0.001f); }
        Sec2IfDushNalBackStenkaDushLights.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float leftLimitPosZ1;
            float rightLimitPosZ2;


            
            if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
            {
                if (!StenkaDushaIn2SectionBanyaTheSamePosAsStenaSDveryu) 
                LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3((XposOfStenkaDusha / 100f) +  TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 0.9f / 2f + TolschinaStenKarkasa);
            }
            else if ((DushPos == "DushPos2") || (DushPos == "DushPos3"))
                LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3((XposOfStenkaDusha / 100f) +  TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 0.9f / 2f + TolschinaStenKarkasa + BanyaWidth / 100f - 0.9f - TolschinaStenKarkasa);




            //    for (int i=0; i<StenkaDusha.GetComponent<WallScript>().WallElements.Count;i++)
            //{
            //    if ( StenkaDusha.GetComponent<WallScript>().WallElements[i]!=null )
            //    {
            //        LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3(LightHelperSec2_IfDushNalBackStenkaDush.transform.position.x, LightHelperSec2_IfDushNalBackStenkaDush.transform.position.y, StenkaDusha.GetComponent<WallScript>().WallElements[i].gameObject.transform.position.z);
            //        break;
            //        //i == StenkaDusha.GetComponent<WallScript>().WallElements.Count;
            //    }
            //}




            leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperSec2_IfDushNalBackStenkaDush.transform.position.z + float.Parse(leftLimit) / 100f * LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.z - LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.z / 2;
            rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperSec2_IfDushNalBackStenkaDush.transform.position.z - (1f - float.Parse(rightLimit) / 100f) * LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.z + LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.z / 2;




            float BetweenLightsDistance;
            if (lightCount != "1")
            {
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(lightCount) - 1);

            }
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject someLamp = GameObject.Instantiate(LampSphericToInstantiate);
                Sec2IfDushNalBackStenkaDushLights.Add(someLamp);
                someLamp.SetActive(true);
                if (lightCount != "1")
                {

                    someLamp.transform.position = new Vector3(LightHelperSec2_IfDushNalBackStenkaDush.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperSec2_IfDushNalBackStenkaDush.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);

                }
                else
                {
                    someLamp.transform.position = new Vector3(LightHelperSec2_IfDushNalBackStenkaDush.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperSec2_IfDushNalBackStenkaDush.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);

                }
                someLamp.transform.eulerAngles = new Vector3(0, 180, 0);

            }
        }





    }




    public void SetLightOpt_S2_LeftWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S2_LeftWall_Count>", "</LightOpt_S2_LeftWall_Count>"), "<LightOpt_S2_LeftWall_Count>" + LeftWallLightCountSec2.text + "</LightOpt_S2_LeftWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_LeftWall_TopLimit>", "</S2_LeftWall_TopLimit>"), "<S2_LeftWall_TopLimit>" + LeftWallLightTopLimitSec2.text + "</S2_LeftWall_TopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_LeftWall_BotLimit>", "</S2_LeftWall_BotLimit>"), "<S2_LeftWall_BotLimit>" + LeftWallLightBotLimitSec2.text + "</S2_LeftWall_BotLimit>");
    }
    public void ManageLightOpt_S2_LeftWall(string lightCount, string topLimit, string botLimit)
    {


        LeftWallLightCountSec2.text = lightCount;
        LeftWallLightTopLimitSec2.text = topLimit;
        LeftWallLightBotLimitSec2.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < LeftWallSection2Lamps.Count; i++)
        { Destroy(LeftWallSection2Lamps[i], 0.001f); }
        LeftWallSection2Lamps.Clear();


        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperLeftWallSection2.transform.position.x + float.Parse(topLimit) / 100f * LightHelperLeftWallSection2.transform.localScale.x - LightHelperLeftWallSection2.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperLeftWallSection2.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperLeftWallSection2.transform.localScale.x + LightHelperLeftWallSection2.transform.localScale.x / 2;

            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampLeftWall2Sec = GameObject.Instantiate(LampSphericToInstantiate);
                LeftWallSection2Lamps.Add(LampLeftWall2Sec);
                LampLeftWall2Sec.SetActive(true);

                if (lightCount != "1")
                    LampLeftWall2Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperLeftWallSection2.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                else
                    LampLeftWall2Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperLeftWallSection2.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                LampLeftWall2Sec.transform.eulerAngles = new Vector3(0, -270, 0);
            }
        }

    }

    public void ManageLightOpt_S2_RightWall(string lightCount, string topLimit, string botLimit)
    {
        RightWallLightCountSec2.text = lightCount;
        RightWallLightTopLimitSec2.text = topLimit;
        RightWallLightBotLimitSec2.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;
        for (int i = 0; i < RightWallSection2Lamps.Count; i++)
        { Destroy(RightWallSection2Lamps[i], 0.001f); }
        RightWallSection2Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperRightWallSection2.transform.position.x + float.Parse(topLimit) / 100f * LightHelperRightWallSection2.transform.localScale.x - LightHelperRightWallSection2.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperRightWallSection2.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperRightWallSection2.transform.localScale.x + LightHelperRightWallSection2.transform.localScale.x / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampRightWall2Sec = GameObject.Instantiate(LampSphericToInstantiate);
                RightWallSection2Lamps.Add(LampRightWall2Sec);
                LampRightWall2Sec.SetActive(true);
                if (lightCount != "1")
                    LampRightWall2Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperRightWallSection2.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                else
                    LampRightWall2Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperRightWallSection2.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                LampRightWall2Sec.transform.eulerAngles = new Vector3(0, -90, 0);
            }
        }

    }
    public void SetLightOpt_S2_RightWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S2_RightWall_Count>", "</LightOpt_S2_RightWall_Count>"), "<LightOpt_S2_RightWall_Count>" + RightWallLightCountSec2.text + "</LightOpt_S2_RightWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_RightWall_TopLimit>", "</S2_RightWall_TopLimit>"), "<S2_RightWall_TopLimit>" + RightWallLightTopLimitSec2.text + "</S2_RightWall_TopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_RightWall_BotLimit>", "</S2_RightWall_BotLimit>"), "<S2_RightWall_BotLimit>" + RightWallLightBotLimitSec2.text + "</S2_RightWall_BotLimit>");
    }

    public void SetLightOpt_S2_BotWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S2_BotWall_Count>", "</LightOpt_S2_BotWall_Count>"), "<LightOpt_S2_BotWall_Count>" + BotWallLightCountSec2.text + "</LightOpt_S2_BotWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_BotWall_LeftLimit>", "</S2_BotWall_LeftLimit>"), "<S2_BotWall_LeftLimit>" + BotWallLightLeftLimitSec2.text + "</S2_BotWall_LeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_BotWall_RightLimit>", "</S2_BotWall_RightLimit>"), "<S2_BotWall_RightLimit>" + BotWallLightRightLimitSec2.text + "</S2_BotWall_RightLimit>");
    }
    public void ManageLightOpt_S2_BotWall(string lightCount, string leftLimit, string rightLimit)
    {
        BotWallLightCountSec2.text = lightCount;
        BotWallLightLeftLimitSec2.text = leftLimit;
        BotWallLightRightLimitSec2.text = rightLimit;
        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < BotWallSection2Lamps.Count; i++)
        { Destroy(BotWallSection2Lamps[i], 0.001f); }
        BotWallSection2Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperBotWallSection2.transform.position.z + float.Parse(leftLimit) / 100f * LightHelperBotWallSection2.transform.localScale.z - LightHelperBotWallSection2.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperBotWallSection2.transform.position.z - (1f - float.Parse(rightLimit) / 100f) * LightHelperBotWallSection2.transform.localScale.z + LightHelperBotWallSection2.transform.localScale.z / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
            {
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(lightCount) - 1);

            }
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampBotWall2Sec = GameObject.Instantiate(LampSphericToInstantiate);
                BotWallSection2Lamps.Add(LampBotWall2Sec);
                LampBotWall2Sec.SetActive(true);
                if (lightCount != "1")
                    LampBotWall2Sec.transform.position = new Vector3(LightHelperBotWallSection2.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection2.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);
                else
                {
                    LampBotWall2Sec.transform.position = new Vector3(LightHelperBotWallSection2.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection2.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);
                }

            }
        }


    }

    public void SetLightOpt_S2_TopWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S2_TopWall_Count>", "</LightOpt_S2_TopWall_Count>"), "<LightOpt_S2_TopWall_Count>" + TopWallLightCountSec2.text + "</LightOpt_S2_TopWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_TopWall_LeftLimit>", "</S2_TopWall_LeftLimit>"), "<S2_TopWall_LeftLimit>" + TopWallLightLeftLimitSec2.text + "</S2_TopWall_LeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S2_TopWall_RightLimit>", "</S2_TopWall_RightLimit>"), "<S2_TopWall_RightLimit>" + TopWallLightRightLimitSec2.text + "</S2_TopWall_RightLimit>");
    }
    public void ManageLightOpt_S2_TopWall(string lightCount, string leftLimit, string rightLimit)
    {

        TopWallLightCountSec2.text = lightCount;
        TopWallLightLeftLimitSec2.text = leftLimit;
        TopWallLightRightLimitSec2.text = rightLimit;


        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < TopWallSection2Lamps.Count; i++)
        { Destroy(TopWallSection2Lamps[i], 0.001f); }
        TopWallSection2Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0"))
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperTopWallSection2.transform.position.z + float.Parse(leftLimit) / 100f * LightHelperTopWallSection2.transform.localScale.z - LightHelperTopWallSection2.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperTopWallSection2.transform.position.z - (1f - float.Parse(rightLimit) / 100f) * LightHelperTopWallSection2.transform.localScale.z + LightHelperTopWallSection2.transform.localScale.z / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
            {
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(lightCount) - 1);

            }
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampTopWall2Sec = GameObject.Instantiate(LampSphericToInstantiate);
                TopWallSection2Lamps.Add(LampTopWall2Sec);
                LampTopWall2Sec.SetActive(true);
                if (lightCount != "1")
                    LampTopWall2Sec.transform.position = new Vector3(LightHelperTopWallSection2.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection2.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);
                else
                {
                    LampTopWall2Sec.transform.position = new Vector3(LightHelperTopWallSection2.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection2.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);
                }
                LampTopWall2Sec.transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }


    }

    #endregion

    #region section3
    public void SetLightOpt_S3_LeftWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S3_LeftWall_Count>", "</LightOpt_S3_LeftWall_Count>"), "<LightOpt_S3_LeftWall_Count>" + LeftWallLightCountSec3.text + "</LightOpt_S3_LeftWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_LeftWall_TopLimit>", "</S3_LeftWall_TopLimit>"), "<S3_LeftWall_TopLimit>" + LeftWallLightTopLimitSec3.text + "</S3_LeftWall_TopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_LeftWall_BotLimit>", "</S3_LeftWall_BotLimit>"), "<S3_LeftWall_BotLimit>" + LeftWallLightBotLimitSec3.text + "</S3_LeftWall_BotLimit>");
    }
    public void ManageLightOpt_S3_LeftWall(string lightCount, string topLimit, string botLimit)
    {



        LeftWallLightCountSec3.text = lightCount;
        LeftWallLightTopLimitSec3.text = topLimit;
        LeftWallLightBotLimitSec3.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < LeftWallSection3Lamps.Count; i++)
        { Destroy(LeftWallSection3Lamps[i], 0.001f); }
        LeftWallSection3Lamps.Clear();


        if ((lightCount.Length != 0) && (lightCount != "0") && SectionCount != 2)
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperLeftWallSection3.transform.position.x + float.Parse(topLimit) / 100f * LightHelperLeftWallSection3.transform.localScale.x - LightHelperLeftWallSection3.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperLeftWallSection3.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperLeftWallSection3.transform.localScale.x + LightHelperLeftWallSection3.transform.localScale.x / 2;

            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampLeftWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                LeftWallSection3Lamps.Add(LampLeftWall3Sec);
                LampLeftWall3Sec.SetActive(true);

                if (lightCount != "1")
                    LampLeftWall3Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperLeftWallSection3.transform.position.y, LightHelperLeftWallSection3.transform.position.z - TolschinaStenKarkasa / 2);
                else
                    LampLeftWall3Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperLeftWallSection3.transform.position.y, LightHelperLeftWallSection3.transform.position.z - TolschinaStenKarkasa / 2);
                LampLeftWall3Sec.transform.eulerAngles = new Vector3(0, -270, 0);
            }
        }

    }

    public void ManageLightOpt_S3_RightWall(string lightCount, string topLimit, string botLimit)
    {

        RightWallLightCountSec3.text = lightCount;
        RightWallLightTopLimitSec3.text = topLimit;
        RightWallLightBotLimitSec3.text = botLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;
        for (int i = 0; i < RightWallSection3Lamps.Count; i++)
        { Destroy(RightWallSection3Lamps[i], 0.001f); }
        RightWallSection3Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0") && SectionCount != 2)
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperRightWallSection3.transform.position.x + float.Parse(topLimit) / 100f * LightHelperRightWallSection3.transform.localScale.x - LightHelperRightWallSection3.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperRightWallSection3.transform.position.x - (1f - float.Parse(botLimit) / 100f) * LightHelperRightWallSection3.transform.localScale.x + LightHelperRightWallSection3.transform.localScale.x / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(lightCount) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampRightWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                RightWallSection3Lamps.Add(LampRightWall3Sec);
                LampRightWall3Sec.SetActive(true);

                if (lightCount != "1")
                    LampRightWall3Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperRightWallSection3.transform.position.y, LightHelperRightWallSection3.transform.position.z + TolschinaStenKarkasa / 2);
                else
                    LampRightWall3Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperRightWallSection3.transform.position.y, LightHelperRightWallSection3.transform.position.z + TolschinaStenKarkasa / 2);
                LampRightWall3Sec.transform.eulerAngles = new Vector3(0, -90, 0);
            }
        }

    }
    public void SetLightOpt_S3_RightWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S3_RightWall_Count>", "</LightOpt_S3_RightWall_Count>"), "<LightOpt_S3_RightWall_Count>" + RightWallLightCountSec3.text + "</LightOpt_S3_RightWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_RightWall_TopLimit>", "</S3_RightWall_TopLimit>"), "<S3_RightWall_TopLimit>" + RightWallLightTopLimitSec3.text + "</S3_RightWall_TopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_RightWall_BotLimit>", "</S3_RightWall_BotLimit>"), "<S3_RightWall_BotLimit>" + RightWallLightBotLimitSec3.text + "</S3_RightWall_BotLimit>");
    }

    public void SetLightOpt_S3_BotWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S3_BotWall_Count>", "</LightOpt_S3_BotWall_Count>"), "<LightOpt_S3_BotWall_Count>" + BotWallLightCountSec3.text + "</LightOpt_S3_BotWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_BotWall_LeftLimit>", "</S3_BotWall_LeftLimit>"), "<S3_BotWall_LeftLimit>" + BotWallLightLeftLimitSec3.text + "</S3_BotWall_LeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_BotWall_RightLimit>", "</S3_BotWall_RightLimit>"), "<S3_BotWall_RightLimit>" + BotWallLightRightLimitSec3.text + "</S3_BotWall_RightLimit>");
    }
    public void ManageLightOpt_S3_BotWall(string lightCount, string leftLimit, string rightLimit)
    {

        BotWallLightCountSec3.text = lightCount;
        BotWallLightLeftLimitSec3.text = leftLimit;
        BotWallLightRightLimitSec3.text = rightLimit;
        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < BotWallSection3Lamps.Count; i++)
        { Destroy(BotWallSection3Lamps[i], 0.001f); }
        BotWallSection3Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0") && SectionCount != 2)
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperBotWallSection3.transform.position.z + float.Parse(leftLimit) / 100f * LightHelperBotWallSection3.transform.localScale.z - LightHelperBotWallSection3.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperBotWallSection3.transform.position.z - (1f - float.Parse(rightLimit) / 100f) * LightHelperBotWallSection3.transform.localScale.z + LightHelperBotWallSection3.transform.localScale.z / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
            {
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(lightCount) - 1);

            }
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampBotWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                BotWallSection3Lamps.Add(LampBotWall3Sec);
                LampBotWall3Sec.SetActive(true);
                if (lightCount != "1")
                {

                    LampBotWall3Sec.transform.position = new Vector3(LightHelperBotWallSection3.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection3.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);


                }
                else
                {
                    LampBotWall3Sec.transform.position = new Vector3(LightHelperBotWallSection3.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection3.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);
                }

            }
        }


    }

    public void SetLightOpt_S3_TopWall()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LightOpt_S3_TopWall_Count>", "</LightOpt_S3_TopWall_Count>"), "<LightOpt_S3_TopWall_Count>" + TopWallLightCountSec3.text + "</LightOpt_S3_TopWall_Count>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_TopWall_LeftLimit>", "</S3_TopWall_LeftLimit>"), "<S3_TopWall_LeftLimit>" + TopWallLightLeftLimitSec3.text + "</S3_TopWall_LeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<S3_TopWall_RightLimit>", "</S3_TopWall_RightLimit>"), "<S3_TopWall_RightLimit>" + TopWallLightRightLimitSec3.text + "</S3_TopWall_RightLimit>");
    }
    public void ManageLightOpt_S3_TopWall(string lightCount, string leftLimit, string rightLimit)
    {
        TopWallLightCountSec3.text = lightCount;
        TopWallLightLeftLimitSec3.text = leftLimit;
        TopWallLightRightLimitSec3.text = rightLimit;

        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;

        for (int i = 0; i < TopWallSection3Lamps.Count; i++)
        { Destroy(TopWallSection3Lamps[i], 0.001f); }
        TopWallSection3Lamps.Clear();

        if ((lightCount.Length != 0) && (lightCount != "0") && SectionCount != 2)
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperTopWallSection3.transform.position.z + float.Parse(leftLimit) / 100f * LightHelperTopWallSection3.transform.localScale.z - LightHelperTopWallSection3.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperTopWallSection3.transform.position.z - (1f - float.Parse(rightLimit) / 100f) * LightHelperTopWallSection3.transform.localScale.z + LightHelperTopWallSection3.transform.localScale.z / 2;



            float BetweenLightsDistance;
            if (lightCount != "1")
            {
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(lightCount) - 1);

            }
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(lightCount); i++)
            {

                GameObject LampTopWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                TopWallSection3Lamps.Add(LampTopWall3Sec);
                LampTopWall3Sec.SetActive(true);
                if (lightCount != "1")
                    LampTopWall3Sec.transform.position = new Vector3(LightHelperTopWallSection3.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection3.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);
                else
                {
                    LampTopWall3Sec.transform.position = new Vector3(LightHelperTopWallSection3.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection3.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);
                }
                LampTopWall3Sec.transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }


    }
    #endregion




    public void TurnOffUnneedLightsAndManageDependencyFromDushPos()

    {

        if ((SectionCount == 2) && (DushNalichie == "NoDush"))
        {


            foreach (GameObject light in Sec2IfDushNalBackStenkaDushLights)
            {
                light.SetActive(false);
            }
                        
            IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(false);

            


            foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
            {
                light.SetActive(false);
            }

            IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(false);





            foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
            {
                light.SetActive(false);
            }


            IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(false);




        }



        if ((SectionCount == 2) && (DushNalichie == "DushEnabled"))
        {





            if (((DushPos == "DushPos1") || (DushPos == "DushPos4")) && PechIndoorOrOutdoor == "Indoor")

            {

                LeftWallLightCountSec2.gameObject.SetActive(false);
                LeftWallLightTopLimitSec2.gameObject.SetActive(false);
                LeftWallLightBotLimitSec2.gameObject.SetActive(false);


                foreach (GameObject light in LeftWallSection2Lamps)
                {
                    light.SetActive(false);

                }

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);

                foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, 90, 0);
                }




                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);

                foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, 90, 0);
                }




                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(true);


                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-69, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-19, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);





            }
            else if (((DushPos == "DushPos2") || (DushPos == "DushPos3")) && PechIndoorOrOutdoor == "Indoor")

            {

                RightWallLightCountSec2.gameObject.SetActive(false);
                RightWallLightTopLimitSec2.gameObject.SetActive(false);
                RightWallLightBotLimitSec2.gameObject.SetActive(false);

                foreach (GameObject light in RightWallSection2Lamps)
                {
                    light.SetActive(false);
                }




                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);



                foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, -90, 0);
                }





                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);




                foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, -90, 0);
                }


                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(true);


                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(19, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(69, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);





            }



            if (((DushPos == "DushPos3") || (DushPos == "DushPos4")) && PechIndoorOrOutdoor == "Outdoor")
            {
                StenkaDusha.SetActive(false);


                foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
                {
                    light.SetActive(false);
                }



                foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
                {
                    light.SetActive(false);
                }


                foreach (GameObject light in Sec2IfDushNalBackStenkaDushLights)
                {
                    light.SetActive(false);
                }


            }




            if (DushPos == "DushPos1" && PechIndoorOrOutdoor == "Outdoor")
            {
                StenkaDusha.SetActive(true);


                LeftWallLightCountSec2.gameObject.SetActive(false);
                LeftWallLightTopLimitSec2.gameObject.SetActive(false);
                LeftWallLightBotLimitSec2.gameObject.SetActive(false);


                foreach (GameObject light in LeftWallSection2Lamps)
                {
                    light.SetActive(false);
                }

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);



                foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperLeftWallSection2.transform.position.z - TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, 90, 0);
                }

                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);




                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(true);


                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-119, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-69, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-19, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);



            }

            if (DushPos == "DushPos2" && PechIndoorOrOutdoor == "Outdoor")
            {
                StenkaDusha.SetActive(true);

                RightWallLightCountSec2.gameObject.SetActive(false);
                RightWallLightTopLimitSec2.gameObject.SetActive(false);
                RightWallLightBotLimitSec2.gameObject.SetActive(false);

                foreach (GameObject light in RightWallSection2Lamps)
                {
                    light.SetActive(false);
                }



                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);

                foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, -90, 0);
                }





                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(true);

                IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);


                foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
                {
                    light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, LightHelperRightWallSection2.transform.position.z + TolschinaStenKarkasa / 2);
                    light.transform.eulerAngles = new Vector3(0, -90, 0);
                }



                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(true);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(true);


                IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(19, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(69, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);
                IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(119, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.y, IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.GetComponent<RectTransform>().anchoredPosition3D.z);









            }




        }







        if (SectionCount == 3)// независимо от наличия душа  (ориентируется по перегородке2)
        {

            IF_Sec2IfDushNalBackStenkaDushLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.gameObject.SetActive(false);

            foreach (GameObject light in Sec2IfDushNalBackStenkaDushLights)
            {
                GameObject.Destroy(light, 0.01f);

            }
            Sec2IfDushNalBackStenkaDushLights.Clear();



            IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromDushToStenda2ShirinaLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.gameObject.SetActive(false);

            foreach (GameObject light in Sec2IfDushNalFromDushToStenda2ShirinaLights)
            {
                GameObject.Destroy(light, 0.01f);



            }
            Sec2IfDushNalFromDushToStenda2ShirinaLights.Clear();


            IF_Sec2IfDushNalFromParilkaToDushBotLimit.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromParilkaToDushLightsCount.gameObject.SetActive(false);
            IF_Sec2IfDushNalFromParilkaToDushTopLimit.gameObject.SetActive(false);

            foreach (GameObject light in Sec2IfDushNalFromParilkaToDushLights)
            {
                GameObject.Destroy(light, 0.01f);
            }
            Sec2IfDushNalFromParilkaToDushLights.Clear();

        }






    }




    public void SetOptionsForLightsInSection2_3()
    {

        //  если есть душ      от парилки до душа
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsCount>", "</Sec2IfDushNalFromParilkaToDushLightsCount>"), "<Sec2IfDushNalFromParilkaToDushLightsCount>" + Sec2IfDushNalFromParilkaToDushLights.Count + "</Sec2IfDushNalFromParilkaToDushLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsTopLimit>", "</Sec2IfDushNalFromParilkaToDushLightsTopLimit>"), "<Sec2IfDushNalFromParilkaToDushLightsTopLimit>" + IF_Sec2IfDushNalFromParilkaToDushTopLimit.text + "</Sec2IfDushNalFromParilkaToDushLightsTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromParilkaToDushLightsBotLimit>", "</Sec2IfDushNalFromParilkaToDushLightsBotLimit>"), "<Sec2IfDushNalFromParilkaToDushLightsBotLimit>" + IF_Sec2IfDushNalFromParilkaToDushBotLimit.text + "</Sec2IfDushNalFromParilkaToDushLightsBotLimit>");
        //

        //  если есть душ      от   душа до стены ширина2
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>" + Sec2IfDushNalFromDushToStenda2ShirinaLights.Count + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>" + IF_Sec2IfDushNalFromDushToStenda2ShirinaTopLimit.text + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>", "</Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>"), "<Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>" + IF_Sec2IfDushNalFromDushToStenda2ShirinaBotLimit.text + "</Sec2IfDushNalFromDushToStenda2ShirinaLightsBotLimit>");
        //

        //  если есть душ       обратная сторону стенки душа
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsCount>", "</Sec2IfDushNalBackStenkaDushLightsCount>"), "<Sec2IfDushNalBackStenkaDushLightsCount>" + Sec2IfDushNalBackStenkaDushLights.Count + "</Sec2IfDushNalBackStenkaDushLightsCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsTopLimit>", "</Sec2IfDushNalBackStenkaDushLightsTopLimit>"), "<Sec2IfDushNalBackStenkaDushLightsTopLimit>" + IF_Sec2IfDushNalBackStenkaDushLightsLeftLimit.text + "</Sec2IfDushNalBackStenkaDushLightsTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2IfDushNalBackStenkaDushLightsBotLimit>", "</Sec2IfDushNalBackStenkaDushLightsBotLimit>"), "<Sec2IfDushNalBackStenkaDushLightsBotLimit>" + IF_Sec2IfDushNalBackStenkaDushLightsRightLimit.text + "</Sec2IfDushNalBackStenkaDushLightsBotLimit>");
        //




        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2TopWallLightCount>", "</Sec2TopWallLightCount>"), "<Sec2TopWallLightCount>" + TopWallSection2Lamps.Count + "</Sec2TopWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2TopWallLightLeftLimit>", "</Sec2TopWallLightLeftLimit>"), "<Sec2TopWallLightLeftLimit>" + TopWallLightLeftLimitSec2.text + "</Sec2TopWallLightLeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2TopWallLightRightLimit>", "</Sec2TopWallLightRightLimit>"), "<Sec2TopWallLightRightLimit>" + TopWallLightRightLimitSec2.text + "</Sec2TopWallLightRightLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2BotWallLightCount>", "</Sec2BotWallLightCount>"), "<Sec2BotWallLightCount>" + BotWallSection2Lamps.Count + "</Sec2BotWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2BotWallLightLeftLimit>", "</Sec2BotWallLightLeftLimit>"), "<Sec2BotWallLightLeftLimit>" + BotWallLightLeftLimitSec2.text + "</Sec2BotWallLightLeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2BotWallLightRightLimit>", "</Sec2BotWallLightRightLimit>"), "<Sec2BotWallLightRightLimit>" + BotWallLightRightLimitSec2.text + "</Sec2BotWallLightRightLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2RightWallLightCount>", "</Sec2RightWallLightCount>"), "<Sec2RightWallLightCount>" + RightWallSection2Lamps.Count + "</Sec2RightWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2RightWallLightTopLimit>", "</Sec2RightWallLightTopLimit>"), "<Sec2RightWallLightTopLimit>" + RightWallLightTopLimitSec2.text + "</Sec2RightWallLightTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2RightWallLightBotLimit>", "</Sec2RightWallLightBotLimit>"), "<Sec2RightWallLightBotLimit>" + RightWallLightBotLimitSec2.text + "</Sec2RightWallLightBotLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2LeftWallLightCount>", "</Sec2LeftWallLightCount>"), "<Sec2LeftWallLightCount>" + LeftWallSection2Lamps.Count + "</Sec2LeftWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2LeftWallLightTopLimit>", "</Sec2LeftWallLightTopLimit>"), "<Sec2LeftWallLightTopLimit>" + LeftWallLightTopLimitSec2.text + "</Sec2LeftWallLightTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec2LeftWallLightBotLimit>", "</Sec2LeftWallLightBotLimit>"), "<Sec2LeftWallLightBotLimit>" + LeftWallLightBotLimitSec2.text + "</Sec2LeftWallLightBotLimit>");



        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3TopWallLightCount>", "</Sec3TopWallLightCount>"), "<Sec3TopWallLightCount>" + TopWallSection3Lamps.Count + "</Sec3TopWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3TopWallLightLeftLimit>", "</Sec3TopWallLightLeftLimit>"), "<Sec3TopWallLightLeftLimit>" + TopWallLightLeftLimitSec3.text + "</Sec3TopWallLightLeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3TopWallLightRightLimit>", "</Sec3TopWallLightRightLimit>"), "<Sec3TopWallLightRightLimit>" + TopWallLightRightLimitSec3.text + "</Sec3TopWallLightRightLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3BotWallLightCount>", "</Sec3BotWallLightCount>"), "<Sec3BotWallLightCount>" + BotWallSection3Lamps.Count + "</Sec3BotWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3BotWallLightLeftLimit>", "</Sec3BotWallLightLeftLimit>"), "<Sec3BotWallLightLeftLimit>" + BotWallLightLeftLimitSec3.text + "</Sec3BotWallLightLeftLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3BotWallLightRightLimit>", "</Sec3BotWallLightRightLimit>"), "<Sec3BotWallLightRightLimit>" + BotWallLightRightLimitSec3.text + "</Sec3BotWallLightRightLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3RightWallLightCount>", "</Sec3RightWallLightCount>"), "<Sec3RightWallLightCount>" + RightWallSection3Lamps.Count + "</Sec3RightWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3RightWallLightTopLimit>", "</Sec3RightWallLightTopLimit>"), "<Sec3RightWallLightTopLimit>" + RightWallLightTopLimitSec3.text + "</Sec3RightWallLightTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3RightWallLightBotLimit>", "</Sec3RightWallLightBotLimit>"), "<Sec3RightWallLightBotLimit>" + RightWallLightBotLimitSec3.text + "</Sec3RightWallLightBotLimit>");

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3LeftWallLightCount>", "</Sec3LeftWallLightCount>"), "<Sec3LeftWallLightCount>" + LeftWallSection3Lamps.Count + "</Sec3LeftWallLightCount>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3LeftWallLightTopLimit>", "</Sec3LeftWallLightTopLimit>"), "<Sec3LeftWallLightTopLimit>" + LeftWallLightTopLimitSec3.text + "</Sec3LeftWallLightTopLimit>");
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Sec3LeftWallLightBotLimit>", "</Sec3LeftWallLightBotLimit>"), "<Sec3LeftWallLightBotLimit>" + LeftWallLightBotLimitSec3.text + "</Sec3LeftWallLightBotLimit>");



        //Sec2TopWallLightCount = TopWallSection2Lamps.Count.ToString();
        //Debug.Log("Sec2TopWallLightCount:" + Sec2TopWallLightCount);
        //Sec2TopWallLightLeftLimit = TopWallLightLeftLimitSec2.text;
        //Sec2TopWallLightRightLimit = TopWallLightRightLimitSec2.text;

        //Sec2BotWallLightCount = BotWallSection2Lamps.Count.ToString();
        //Sec2BotWallLightLeftLimit = BotWallLightLeftLimitSec2.text;
        //Sec2BotWallLightRightLimit = BotWallLightRightLimitSec2.text;


        //Sec2RightWallLightCount = RightWallSection2Lamps.Count.ToString();
        //Sec2RightWallLightTopLimit = RightWallLightTopLimitSec2.text;
        //Sec2RightWallLightBotLimit = RightWallLightBotLimitSec2.text;


        //Sec2LeftWallLightCount = LeftWallSection2Lamps.Count.ToString();
        //Sec2LeftWallLightTopLimit = LeftWallLightTopLimitSec2.text;
        //Sec2LeftWallLightBotLimit = LeftWallLightBotLimitSec2.text;




        //Sec3TopWallLightCount = TopWallSection3Lamps.Count.ToString();
        //Sec3TopWallLightLeftLimit = TopWallLightLeftLimitSec3.text;
        //Sec3TopWallLightRightLimit = TopWallLightRightLimitSec3.text;

        //Sec3BotWallLightCount = BotWallSection3Lamps.Count.ToString();
        //Sec3BotWallLightLeftLimit = BotWallLightLeftLimitSec3.text;
        //Sec3BotWallLightRightLimit = BotWallLightRightLimitSec3.text;


        //Sec3RightWallLightCount = RightWallSection3Lamps.Count.ToString();
        //Sec3RightWallLightTopLimit = RightWallLightTopLimitSec3.text;
        //Sec3RightWallLightBotLimit = RightWallLightBotLimitSec3.text;


        //Sec3LeftWallLightCount = LeftWallSection3Lamps.Count.ToString();
        //Sec3LeftWallLightTopLimit = LeftWallLightTopLimitSec3.text;
        //Sec3LeftWallLightBotLimit = LeftWallLightBotLimitSec3.text;




    }



    public void AddMustHaveMetricLinesEndStartPositions()
    {

        #region metric skams

        
        if (TopSkam1.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3( TopSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, TopSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin), 
            new Vector3(TopSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, TopSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, TopSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax) ,
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam,MetricLine.ParentObjTypeEnum.Skam));
        }

        if (TopSkam2.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3(TopSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, TopSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin),
            new Vector3(TopSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, TopSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, TopSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax),
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }




        if (BotSkam1.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3(BotSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, BotSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMin),
            new Vector3(BotSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, BotSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, BotSkam1GO.GetComponent<SkamScript>().Sid_Z_LimitMax),
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }

        if (BotSkam2.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3(BotSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, BotSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMin),
            new Vector3(BotSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.x, BotSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, BotSkam2GO.GetComponent<SkamScript>().Sid_Z_LimitMax),
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }









        if (LeftSkam1.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3(LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin, LeftSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, LeftSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
            new Vector3(LeftSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax, LeftSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, LeftSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }

        if (LeftSkam2.isOn)
        {
            MetricLines.Add(new MetricLine(
           new Vector3(LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin, LeftSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, LeftSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
           new Vector3(LeftSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax, LeftSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, LeftSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
           MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }




        if (RightSkam1.isOn)
        {
            MetricLines.Add(new MetricLine(
            new Vector3(RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMin, RightSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, RightSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
            new Vector3(RightSkam1GO.GetComponent<SkamScript>().Sid_X_LimitMax, RightSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, RightSkam1GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
            MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }

        if (RightSkam2.isOn)
        {
            MetricLines.Add(new MetricLine(
           new Vector3(RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMin, RightSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, RightSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
           new Vector3(RightSkam2GO.GetComponent<SkamScript>().Sid_X_LimitMax, RightSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.y, RightSkam2GO.GetComponent<SkamScript>().SidushkaDoski[0].transform.position.z),
           MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam, MetricLine.ParentObjTypeEnum.Skam));
        }















        #endregion











        //добавление маст хэв точек по стене 1 длина
        MetricLines.Add(new MetricLine(StenaDlina1.GetComponent<WallScript>().WallCube1.transform.position, StenaDlina1.GetComponent<WallScript>().WallCube2.transform.position, MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.LeftCam));
        MetricLines.Add(new MetricLine(StenaDlina1.GetComponent<WallScript>().WallCube2.transform.position, StenaDlina1.GetComponent<WallScript>().WallCube3.transform.position, MetricLine.TextSideEnum.Left, MetricLine.VisCamEnum.LeftCam));


        

        for (int i = 0; i < StenaDlina1.GetComponent<WallScript>().Holes.Count; i++)
        {

            MetricLines.Add(new MetricLine(StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube1.transform.position, StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position, MetricLine.TextSideEnum.Bot, MetricLine.VisCamEnum.LeftCam));
            MetricLines.Add(new MetricLine(StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position, StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.LeftCam));
            
        }



        //добавление маст хэв точек по стене 2 длина

        MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube1.transform.position, StenaDlina2.GetComponent<WallScript>().WallCube2.transform.position,MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.RightCam));
        MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube1.transform.position, StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position,MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.RightCam));
        
         

        for (int i = 0; i < StenaDlina2.GetComponent<WallScript>().Holes.Count; i++)
        {
            MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube1.transform.position, StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position,MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.RightCam));

            MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position, StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.RightCam));
             
        }



        //добавление маст хэв точек по стене 1 ширина
        
        MetricLines.Add(new MetricLine(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position + new Vector3(0, 0, TolschinaStenKarkasa ), StenaShirina1_Parilka.GetComponent<WallScript>().WallCube2.transform.position - new Vector3(0, 0, TolschinaStenKarkasa ), MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.TopCam));
        MetricLines.Add(new MetricLine(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position + new Vector3(0, 0, TolschinaStenKarkasa ), StenaShirina1_Parilka.GetComponent<WallScript>().WallCube4.transform.position + new Vector3(0, 0, TolschinaStenKarkasa ), MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.TopCam));
         

        for (int i = 0; i < StenaShirina1_Parilka.GetComponent<WallScript>().Holes.Count; i++)
        {
           
            MetricLines.Add(new MetricLine(StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube1.transform.position , StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position  , MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.TopCam));
            MetricLines.Add(new MetricLine(StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position , StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position, MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.TopCam));
            
        }


        //добавление маст хэв точек по стене 2 ширина  с дверью

        MetricLines.Add(new MetricLine(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube4.transform.position+ new Vector3(0, 0, TolschinaStenKarkasa), StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube3.transform.position - new Vector3(0, 0, TolschinaStenKarkasa), MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.BotCam));
        MetricLines.Add(new MetricLine(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube2.transform.position - new Vector3(0, 0, TolschinaStenKarkasa), StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube3.transform.position - new Vector3(0, 0, TolschinaStenKarkasa), MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.BotCam));


        for (int i = 0; i < StenaShirina2_SDveryu.GetComponent<WallScript>().Holes.Count; i++)
        {
            MetricLines.Add(new MetricLine(StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Bot,MetricLine.VisCamEnum.BotCam));

            MetricLines.Add(new MetricLine(StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube2.transform.position, StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Left,MetricLine.VisCamEnum.BotCam));
            
        }




        //добавление маст хэв точек по сверху

        MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position + new Vector3(0, 0, TolschinaStenKarkasa / 2f), StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(0, 0, TolschinaStenKarkasa / 2f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));

        MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(0, 0, TolschinaStenKarkasa / 2f), StenaDlina1.GetComponent<WallScript>().WallCube3.transform.position - new Vector3(0, 0, TolschinaStenKarkasa / 2f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));



        ///длина парилки

        MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa + XPosOfPeregorodkaParilnogo / 100, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f), StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));



        if (SectionCount == 3)
        {    ///длина 2 секции

            MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa * 2 + XPosOfPeregorodkaMoechnoi / 100f, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f), StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa * 2 + XPosOfPeregorodkaParilnogo / 100f, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));



            ///длина 3 секции
            MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position + new Vector3(-TolschinaStenKarkasa, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f), StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa * 3 + XPosOfPeregorodkaMoechnoi / 100f, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));

        } else
          if (SectionCount == 2)
            {   ///длина 2 секции

                MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position + new Vector3(-TolschinaStenKarkasa, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f), StenaDlina2.GetComponent<WallScript>().WallCube3.transform.position + new Vector3(TolschinaStenKarkasa * 2 + XPosOfPeregorodkaParilnogo / 100f, 0, TolschinaStenKarkasa / 2f - BanyaWidth / 200f),MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));


                //тут
                if (StenkaDusha.activeSelf)
                {
                   if (StenkaDushaRightOrLeft=="Left")
                    {
                    MetricLines.Add(new MetricLine(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position + new Vector3(TolschinaStenKarkasa / 2f, 0, 0), new Vector3(BanyaLength / 100f - TolschinaStenKarkasa, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.y, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.z), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
                    MetricLines.Add(new MetricLine(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position - new Vector3(TolschinaStenKarkasa / 2f, 0, 0), new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + 0.5f * TolschinaStenKarkasa, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.y, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.z), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
                    }

                   else if (StenkaDushaRightOrLeft == "Right")
                {
                    MetricLines.Add(new MetricLine(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position + new Vector3(TolschinaStenKarkasa / 2f, 0, 0), new Vector3(BanyaLength / 100f - TolschinaStenKarkasa, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.y, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.z), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
                    MetricLines.Add(new MetricLine(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position - new Vector3(TolschinaStenKarkasa / 2f, 0, 0), new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + 0.5f * TolschinaStenKarkasa, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.y, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.z), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
                    }

                }

            }


        for (int i = 0; i < Peregorodka1.GetComponent<WallScript>().Holes.Count; i++)
        {
            MetricLines.Add(new MetricLine(Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));
            MetricLines.Add(new MetricLine(Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, new Vector3( Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position.x, Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position.y,BanyaWidth/100f-TolschinaStenKarkasa), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
            MetricLines.Add(new MetricLine(new Vector3( Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position.x, Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position.y,  TolschinaStenKarkasa),   Peregorodka1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position, MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));

        }

        if (SectionCount == 3)
            for (int i = 0; i < Peregorodka2.GetComponent<WallScript>().Holes.Count; i++)
            {

                MetricLines.Add(new MetricLine(Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));


                MetricLines.Add(new MetricLine(Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, new Vector3(Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position.x, Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position.y, BanyaWidth / 100f - TolschinaStenKarkasa), MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
                MetricLines.Add(new MetricLine(new Vector3(Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position.x, Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position.y, TolschinaStenKarkasa), Peregorodka2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position, MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));


            }

        /*   окна стены  в виде сверху
        for (int i = 0; i < StenaDlina1.GetComponent<WallScript>().Holes.Count; i++)
        {
            MetricLines.Add(new MetricLine(StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, StenaDlina1.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position, MetricLine.TextSideEnum.Top, MetricLine.VisCamEnum.UpCam));
        }*/

        /*   окна стены  в виде сверху
       for (int i = 0; i < StenaDlina2.GetComponent<WallScript>().Holes.Count; i++)
       {

           MetricLines.Add(new MetricLine(StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, StenaDlina2.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));

       }*/

        /*  окна стены  в виде сверху
       for (int i = 0; i < StenaShirina1_Parilka.GetComponent<WallScript>().Holes.Count; i++)
       {
           MetricLines.Add(new MetricLine(StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));

       }
       */
        /*  окна стены  в виде сверху
        for (int i = 0; i < StenaShirina2_SDveryu.GetComponent<WallScript>().Holes.Count; i++)
        {

            MetricLines.Add(new MetricLine(StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube4.transform.position, StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].GetComponent<HoleScript>().HoleCube3.transform.position,MetricLine.TextSideEnum.Top,MetricLine.VisCamEnum.UpCam));

        }

        */





    }

    public void UIShowLightInSection2_3ConfigPanel()
   {
       PanelLightConfigInSection2_3.SetActive(true);


       if ((SectionCount == 2) && (DushNalichie == "DushEnabled"))
       {
           ///////
           LightHelperSec2_IfDushNalBackStenkaDush.SetActive(true);
           LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3((XposOfStenkaDusha / 100f) + 3.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 0.9f / 2f + TolschinaStenKarkasa);
           LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale = new Vector3(LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.x, LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.y, 0.9f);

           ///////
           LightHelperSec2_IfDushNalFromParilkaToDush.SetActive(true);
           LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale = new Vector3((XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.y, LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.z);


           /*if ((DushPos == "DushPos2")|| (DushPos == "DushPos3"))
                     LightHelperSec2_IfDushNalFromParilkaToDush.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
               if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
                     LightHelperSec2_IfDushNalFromParilkaToDush.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f,   1.5f * TolschinaStenKarkasa);
        */


        ///////


        LightHelperSec2_IfDushNalFromDushToStenda2Shirina.SetActive(true);
          //  LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale = new Vector3((BanyaLength / 100f - XposOfStenkaDusha / 100f - 4 * TolschinaStenKarkasa), LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.y, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.z);
            
        
        
        
        
        
        /*if ((DushPos == "DushPos2") || (DushPos == "DushPos3"))
                LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position = new Vector3((XposOfStenkaDusha / 100f + (BanyaLength / 100f - XposOfStenkaDusha / 100f) / 2f) +  TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
                LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position = new Vector3((XposOfStenkaDusha / 100f + (BanyaLength / 100f - XposOfStenkaDusha / 100f) / 2f) +  TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            */
        }





        //////Section2
        LightHelperTopWallSection2.SetActive(true);
        LightHelperTopWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f) + 2.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
        LightHelperTopWallSection2.transform.localScale = new Vector3(LightHelperTopWallSection2.transform.localScale.x, LightHelperTopWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);

        /////Section2
        LightHelperBotWallSection2.SetActive(true);
        if (SectionCount == 3)
        {
            LightHelperBotWallSection2.transform.position = new Vector3((XPosOfPeregorodkaMoechnoi / 100f) + 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection2.transform.localScale = new Vector3(LightHelperBotWallSection2.transform.localScale.x, LightHelperBotWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);
        }
        else
        {
            LightHelperBotWallSection2.transform.position = new Vector3((BanyaLength / 100f) - 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection2.transform.localScale = new Vector3(LightHelperBotWallSection2.transform.localScale.x, LightHelperBotWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);
        }


        //////Section2
        LightHelperRightWallSection2.SetActive(true);

        if (SectionCount == 3)
        {
            LightHelperRightWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection2.transform.localScale = new Vector3((XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperRightWallSection2.transform.localScale.y, LightHelperRightWallSection2.transform.localScale.z);
        }
        else
        {

            //if ((DushNalichie == "DushEnabled"))
            LightHelperRightWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 0.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection2.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) - 3f * TolschinaStenKarkasa, LightHelperRightWallSection2.transform.localScale.y, LightHelperRightWallSection2.transform.localScale.z);
        }


        ///////Section2


        LightHelperLeftWallSection2.SetActive(true);

        if (SectionCount == 3)
        {
            LightHelperLeftWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection2.transform.localScale = new Vector3((XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperLeftWallSection2.transform.localScale.y, LightHelperLeftWallSection2.transform.localScale.z);
        }
        else
        {


            LightHelperLeftWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 0.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection2.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) - 3f * TolschinaStenKarkasa, LightHelperLeftWallSection2.transform.localScale.y, LightHelperLeftWallSection2.transform.localScale.z);
        }





        //////Section3
        if (SectionCount == 3)
        {
            LightHelperBotWallSection3.SetActive(true);
            LightHelperBotWallSection3.transform.position = new Vector3((BanyaLength / 100f) - 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection3.transform.localScale = new Vector3(LightHelperBotWallSection3.transform.localScale.x, LightHelperBotWallSection3.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);


            LightHelperTopWallSection3.SetActive(true);
            LightHelperTopWallSection3.transform.position = new Vector3(((XPosOfPeregorodkaMoechnoi) / 100f) + 3.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperTopWallSection3.transform.localScale = new Vector3(LightHelperTopWallSection3.transform.localScale.x, LightHelperTopWallSection3.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);


            LightHelperRightWallSection3.SetActive(true);
            LightHelperRightWallSection3.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f + (BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) / 2f + TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection3.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) - TolschinaStenKarkasa * 4, LightHelperRightWallSection3.transform.localScale.y, LightHelperRightWallSection3.transform.localScale.z);

            LightHelperLeftWallSection3.SetActive(true);
            LightHelperLeftWallSection3.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f + (BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) / 2f + TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection3.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) - TolschinaStenKarkasa * 4, LightHelperLeftWallSection3.transform.localScale.y, LightHelperLeftWallSection3.transform.localScale.z);
        }
        else if (SectionCount == 2)
        {
            LightHelperBotWallSection3.SetActive(false);
            LightHelperTopWallSection3.SetActive(false);
            LightHelperRightWallSection3.SetActive(false);
            LightHelperLeftWallSection3.SetActive(false);
        }

        if (SectionCount == 3)
        {

            DropdownSelectionOfSectionForLightGeneration.interactable = true;
        }
        else
        {
            DropdownSelectionOfSectionForLightGeneration.value = 0;
            DropdownSelectionOfSectionForLightGeneration.interactable = false;
        }

    }


    public void CalculateLightHelpersPositions()
    {
      
        if ((SectionCount == 2) && (DushNalichie == "DushEnabled"))
        {
            ///////
            LightHelperSec2_IfDushNalBackStenkaDush.SetActive(true);
            if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
                LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3((XposOfStenkaDusha / 100f) + 3.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 0.9f / 2f + TolschinaStenKarkasa + BanyaWidth / 100f - 0.9f);

            else if ((DushPos == "DushPos2") || (DushPos == "DushPos3"))
                LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3((XposOfStenkaDusha / 100f) + 3.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 0.9f / 2f + TolschinaStenKarkasa);
            LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale = new Vector3(LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.x, LightHelperSec2_IfDushNalBackStenkaDush.transform.localScale.y, 0.9f);

            ///////
            LightHelperSec2_IfDushNalFromParilkaToDush.SetActive(true);
            LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale = new Vector3((XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.y, LightHelperSec2_IfDushNalFromParilkaToDush.transform.localScale.z);
            if ((DushPos == "DushPos2") || (DushPos == "DushPos3"))
                LightHelperSec2_IfDushNalFromParilkaToDush.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
                LightHelperSec2_IfDushNalFromParilkaToDush.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XposOfStenkaDusha / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            ///////


            LightHelperSec2_IfDushNalFromDushToStenda2Shirina.SetActive(true);
            LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale = new Vector3((BanyaLength / 100f - XposOfStenkaDusha / 100f -  1.5f*TolschinaStenKarkasa), LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.y, LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.localScale.z);
            if ((DushPos == "DushPos2") || (DushPos == "DushPos3"))
                LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position = new Vector3((XposOfStenkaDusha / 100f + (BanyaLength / 100f - XposOfStenkaDusha / 100f) / 2f)-0.25f* TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            if ((DushPos == "DushPos1") || (DushPos == "DushPos4"))
                LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position = new Vector3((XposOfStenkaDusha / 100f + (BanyaLength / 100f - XposOfStenkaDusha / 100f) / 2f)-0.25f* TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);



            
            if (StenkaDushaIn2SectionBanyaTheSamePosAsStenaSDveryu)
            {
                LightHelperSec2_IfDushNalBackStenkaDush.transform.position = new Vector3(100000f, 100000f, 100000f);
                LightHelperSec2_IfDushNalFromDushToStenda2Shirina.transform.position = new Vector3(100000f, 100000f, 100000f);
            }

        }




        LightHelperTopWallSection2.SetActive(true);
        LightHelperTopWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f) + 2.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
        LightHelperTopWallSection2.transform.localScale = new Vector3(LightHelperTopWallSection2.transform.localScale.x, LightHelperTopWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);

        /////Section2
        LightHelperBotWallSection2.SetActive(true);
        if (SectionCount == 3)
        {
            LightHelperBotWallSection2.transform.position = new Vector3((XPosOfPeregorodkaMoechnoi / 100f) + 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection2.transform.localScale = new Vector3(LightHelperBotWallSection2.transform.localScale.x, LightHelperBotWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);
        }
        else
        {
            LightHelperBotWallSection2.transform.position = new Vector3((BanyaLength / 100f) - 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection2.transform.localScale = new Vector3(LightHelperBotWallSection2.transform.localScale.x, LightHelperBotWallSection2.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);
        }


        //////Section2
        LightHelperRightWallSection2.SetActive(true);

        if (SectionCount == 3)
        {
            LightHelperRightWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection2.transform.localScale = new Vector3((XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperRightWallSection2.transform.localScale.y, LightHelperRightWallSection2.transform.localScale.z);
        }
        else
        {


            LightHelperRightWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 0.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection2.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) - 3f * TolschinaStenKarkasa, LightHelperRightWallSection2.transform.localScale.y, LightHelperRightWallSection2.transform.localScale.z);
        }


        ///////Section2


        LightHelperLeftWallSection2.SetActive(true);

        if (SectionCount == 3)
        {
            LightHelperLeftWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 2f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection2.transform.localScale = new Vector3((XPosOfPeregorodkaMoechnoi / 100f - XPosOfPeregorodkaParilnogo / 100f), LightHelperLeftWallSection2.transform.localScale.y, LightHelperLeftWallSection2.transform.localScale.z);
        }
        else
        {


            LightHelperLeftWallSection2.transform.position = new Vector3((XPosOfPeregorodkaParilnogo / 100f + (BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) / 2f) + 0.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection2.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaParilnogo / 100f) - 3f * TolschinaStenKarkasa, LightHelperLeftWallSection2.transform.localScale.y, LightHelperLeftWallSection2.transform.localScale.z);
        }





        //////Section3
        if (SectionCount == 3)
        {
            LightHelperBotWallSection3.SetActive(true);
            LightHelperBotWallSection3.transform.position = new Vector3((BanyaLength / 100f) - 1.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperBotWallSection3.transform.localScale = new Vector3(LightHelperBotWallSection3.transform.localScale.x, LightHelperBotWallSection3.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);


            LightHelperTopWallSection3.SetActive(true);
            LightHelperTopWallSection3.transform.position = new Vector3(((XPosOfPeregorodkaMoechnoi) / 100f) + 3.5f * TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) / 2f);
            LightHelperTopWallSection3.transform.localScale = new Vector3(LightHelperTopWallSection3.transform.localScale.x, LightHelperTopWallSection3.transform.localScale.y, (BanyaWidth / 100f) - 2 * TolschinaStenKarkasa);


            LightHelperRightWallSection3.SetActive(true);
            LightHelperRightWallSection3.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f + (BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) / 2f + TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, (BanyaWidth / 100f) - 1.5f * TolschinaStenKarkasa);
            LightHelperRightWallSection3.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) - TolschinaStenKarkasa * 4, LightHelperRightWallSection3.transform.localScale.y, LightHelperRightWallSection3.transform.localScale.z);

            LightHelperLeftWallSection3.SetActive(true);
            LightHelperLeftWallSection3.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f + (BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) / 2f + TolschinaStenKarkasa, (BanyaHeight / 100f) - 0.15f, 1.5f * TolschinaStenKarkasa);
            LightHelperLeftWallSection3.transform.localScale = new Vector3((BanyaLength / 100f - XPosOfPeregorodkaMoechnoi / 100f) - TolschinaStenKarkasa * 4, LightHelperLeftWallSection3.transform.localScale.y, LightHelperLeftWallSection3.transform.localScale.z);
        }
        else if (SectionCount == 2)
        {
            LightHelperBotWallSection3.SetActive(false);
            LightHelperTopWallSection3.SetActive(false);
            LightHelperRightWallSection3.SetActive(false);
            LightHelperLeftWallSection3.SetActive(false);
        }

        if (SectionCount == 3)
        {

            DropdownSelectionOfSectionForLightGeneration.interactable = true;
        }
        else
        {
            DropdownSelectionOfSectionForLightGeneration.value = 0;
            DropdownSelectionOfSectionForLightGeneration.interactable = false;
        }

    }



    public void UIHideLightInSection2_3ConfigPanel()
    {
        PanelLightConfigInSection2_3.SetActive(false);



        LightHelperSec2_IfDushNalBackStenkaDush.SetActive(false);
        LightHelperSec2_IfDushNalFromParilkaToDush.SetActive(false);
        LightHelperSec2_IfDushNalFromDushToStenda2Shirina.SetActive(false);

        LightHelperTopWallSection2.SetActive(false);
        LightHelperBotWallSection2.SetActive(false);
        LightHelperRightWallSection2.SetActive(false);
        LightHelperLeftWallSection2.SetActive(true);

        LightHelperTopWallSection3.SetActive(false);
        LightHelperBotWallSection3.SetActive(false);
        LightHelperRightWallSection3.SetActive(false);
        LightHelperLeftWallSection3.SetActive(false);
    }


    public void OnConfigLightInSec23ChangeDropdownManageUI()
    {


        if (DropdownSelectionOfSectionForLightGeneration.value == 0)
        {
            TopWallLightCountSec2.gameObject.SetActive(true);
            TopWallLightLeftLimitSec2.gameObject.SetActive(true);
            TopWallLightRightLimitSec2.gameObject.SetActive(true);

            BotWallLightCountSec2.gameObject.SetActive(true);
            BotWallLightLeftLimitSec2.gameObject.SetActive(true);
            BotWallLightRightLimitSec2.gameObject.SetActive(true);

            RightWallLightCountSec2.gameObject.SetActive(true);
            RightWallLightTopLimitSec2.gameObject.SetActive(true);
            RightWallLightBotLimitSec2.gameObject.SetActive(true);

            LeftWallLightCountSec2.gameObject.SetActive(true);
            LeftWallLightTopLimitSec2.gameObject.SetActive(true);
            LeftWallLightBotLimitSec2.gameObject.SetActive(true);



            TopWallLightCountSec3.gameObject.SetActive(false);
            TopWallLightLeftLimitSec3.gameObject.SetActive(false);
            TopWallLightRightLimitSec3.gameObject.SetActive(false);

            BotWallLightCountSec3.gameObject.SetActive(false);
            BotWallLightLeftLimitSec3.gameObject.SetActive(false);
            BotWallLightRightLimitSec3.gameObject.SetActive(false);

            RightWallLightCountSec3.gameObject.SetActive(false);
            RightWallLightTopLimitSec3.gameObject.SetActive(false);
            RightWallLightBotLimitSec3.gameObject.SetActive(false);

            LeftWallLightCountSec3.gameObject.SetActive(false);
            LeftWallLightTopLimitSec3.gameObject.SetActive(false);
            LeftWallLightBotLimitSec3.gameObject.SetActive(false);


        }
        else
            if (DropdownSelectionOfSectionForLightGeneration.value == 1)
        {
            TopWallLightCountSec2.gameObject.SetActive(false);
            TopWallLightLeftLimitSec2.gameObject.SetActive(false);
            TopWallLightRightLimitSec2.gameObject.SetActive(false);

            BotWallLightCountSec2.gameObject.SetActive(false);
            BotWallLightLeftLimitSec2.gameObject.SetActive(false);
            BotWallLightRightLimitSec2.gameObject.SetActive(false);



            RightWallLightCountSec2.gameObject.SetActive(false);
            RightWallLightTopLimitSec2.gameObject.SetActive(false);
            RightWallLightBotLimitSec2.gameObject.SetActive(false);


            LeftWallLightCountSec2.gameObject.SetActive(false);
            LeftWallLightTopLimitSec2.gameObject.SetActive(false);
            LeftWallLightBotLimitSec2.gameObject.SetActive(false);




            TopWallLightCountSec3.gameObject.SetActive(true);
            TopWallLightLeftLimitSec3.gameObject.SetActive(true);
            TopWallLightRightLimitSec3.gameObject.SetActive(true);

            BotWallLightCountSec3.gameObject.SetActive(true);
            BotWallLightLeftLimitSec3.gameObject.SetActive(true);
            BotWallLightRightLimitSec3.gameObject.SetActive(true);

            RightWallLightCountSec3.gameObject.SetActive(true);
            RightWallLightTopLimitSec3.gameObject.SetActive(true);
            RightWallLightBotLimitSec3.gameObject.SetActive(true);

            LeftWallLightCountSec3.gameObject.SetActive(true);
            LeftWallLightTopLimitSec3.gameObject.SetActive(true);
            LeftWallLightBotLimitSec3.gameObject.SetActive(true);


        }








    }

    public void ManageOptionsForLightsInSection2_3//неиспользуется
        (
        string sec2TopWallLightCount, string sec2TopWallLightLeftLimit, string sec2TopWallLightRightLimit,
        string sec2BotWallLightCount, string sec2BotWallLightLeftLimit, string sec2BotWallLightRightLimit,
        string sec2RightWallLightCount, string sec2RightWallLightTopLimit, string sec2RightWallLightBotLimit,
        string sec2LeftWallLightCount, string sec2LeftWallLightTopLimit, string sec2LeftWallLightBotLimit


        , string sec3TopWallLightCount, string sec3TopWallLightLeftLimit, string sec3TopWallLightRightLimit,
        string sec3BotWallLightCount, string sec3BotWallLightLeftLimit, string sec3BotWallLightRightLimit,
        string sec3RightWallLightCount, string sec3RightWallLightTopLimit, string sec3RightWallLightBotLimit,
        string sec3LeftWallLightCount, string sec3LeftWallLightTopLimit, string sec3LeftWallLightBotLimit



        )
    {


        float ShirinaLampy = LampBoundingBox.GetComponent<Renderer>().bounds.size.z;








        ////////////////////////////////////////

        for (int i = 0; i < TopWallSection3Lamps.Count; i++)
        { Destroy(TopWallSection3Lamps[i], 0.001f); }
        TopWallSection3Lamps.Clear();

        for (int i = 0; i < BotWallSection3Lamps.Count; i++)
        { Destroy(BotWallSection3Lamps[i], 0.001f); }
        BotWallSection3Lamps.Clear();

        for (int i = 0; i < RightWallSection3Lamps.Count; i++)
        { Destroy(RightWallSection3Lamps[i], 0.001f); }
        RightWallSection3Lamps.Clear();

        for (int i = 0; i < LeftWallSection3Lamps.Count; i++)
        { Destroy(LeftWallSection3Lamps[i], 0.001f); }
        LeftWallSection3Lamps.Clear();


        if ((TopWallLightCountSec3.text.Length != 0) && (TopWallLightCountSec3.text != "0"))
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperTopWallSection3.transform.position.z + float.Parse(TopWallLightLeftLimitSec3.text) / 100f * LightHelperTopWallSection3.transform.localScale.z - LightHelperTopWallSection3.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperTopWallSection3.transform.position.z - (1f - float.Parse(TopWallLightRightLimitSec3.text) / 100f) * LightHelperTopWallSection3.transform.localScale.z + LightHelperTopWallSection3.transform.localScale.z / 2;


            float BetweenLightsDistance;
            if (TopWallLightCountSec3.text != "1")
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(TopWallLightCountSec3.text) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(TopWallLightCountSec3.text); i++)
            {

                GameObject LampTopWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                TopWallSection3Lamps.Add(LampTopWall3Sec);
                LampTopWall3Sec.SetActive(true);
                if (TopWallLightCountSec3.text != "1")
                    LampTopWall3Sec.transform.position = new Vector3(LightHelperTopWallSection3.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection3.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);
                else
                    LampTopWall3Sec.transform.position = new Vector3(LightHelperTopWallSection3.transform.position.x - TolschinaStenKarkasa / 2f, LightHelperTopWallSection3.transform.position.y, (rightLimitPosZ2 + leftLimitPosZ1) / 2f);
                LampTopWall3Sec.transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }



        if ((BotWallLightCountSec3.text.Length != 0) && (BotWallLightCountSec3.text != "0"))
        {

            float leftLimitPosZ1 = ShirinaLampy / 2f + LightHelperBotWallSection3.transform.position.z + float.Parse(BotWallLightLeftLimitSec3.text) / 100f * LightHelperBotWallSection3.transform.localScale.z - LightHelperBotWallSection3.transform.localScale.z / 2;
            float rightLimitPosZ2 = -ShirinaLampy / 2f + LightHelperBotWallSection3.transform.position.z - (1f - float.Parse(BotWallLightRightLimitSec3.text) / 100f) * LightHelperBotWallSection3.transform.localScale.z + LightHelperBotWallSection3.transform.localScale.z / 2;



            float BetweenLightsDistance;
            if (BotWallLightCountSec3.text != "1")
                BetweenLightsDistance = (rightLimitPosZ2 - leftLimitPosZ1) / (float.Parse(BotWallLightCountSec3.text) - 1);
            else
                BetweenLightsDistance = 0;

            for (int i = 0; i < int.Parse(BotWallLightCountSec3.text); i++)
            {

                GameObject LampBotWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                BotWallSection3Lamps.Add(LampBotWall3Sec);
                LampBotWall3Sec.SetActive(true);
                if (BotWallLightCountSec3.text != "1")
                    LampBotWall3Sec.transform.position = new Vector3(LightHelperBotWallSection3.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection3.transform.position.y, leftLimitPosZ1 + i * BetweenLightsDistance);
                else
                    LampBotWall3Sec.transform.position = new Vector3(LightHelperBotWallSection3.transform.position.x + TolschinaStenKarkasa / 2f, LightHelperBotWallSection3.transform.position.y, (leftLimitPosZ1 + rightLimitPosZ2) / 2f);

            }
        }




        if ((RightWallLightCountSec3.text.Length != 0) && (RightWallLightCountSec3.text != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperRightWallSection3.transform.position.x + float.Parse(RightWallLightTopLimitSec3.text) / 100f * LightHelperRightWallSection3.transform.localScale.x - LightHelperRightWallSection3.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperRightWallSection3.transform.position.x - (1f - float.Parse(RightWallLightBotLimitSec3.text) / 100f) * LightHelperRightWallSection3.transform.localScale.x + LightHelperRightWallSection3.transform.localScale.x / 2;


            float BetweenLightsDistance;
            if (RightWallLightCountSec3.text != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(RightWallLightCountSec3.text) - 1);
            else
                BetweenLightsDistance = 0;


            for (int i = 0; i < int.Parse(RightWallLightCountSec3.text); i++)
            {

                GameObject LampRightWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                RightWallSection3Lamps.Add(LampRightWall3Sec);
                LampRightWall3Sec.SetActive(true);
                if (RightWallLightCountSec3.text != "1")
                    LampRightWall3Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperRightWallSection3.transform.position.y, LightHelperRightWallSection3.transform.position.z + TolschinaStenKarkasa / 2);
                else
                    LampRightWall3Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperRightWallSection3.transform.position.y, LightHelperRightWallSection3.transform.position.z + TolschinaStenKarkasa / 2);
                LampRightWall3Sec.transform.eulerAngles = new Vector3(0, -90, 0);
            }
        }


        if ((LeftWallLightCountSec3.text.Length != 0) && (LeftWallLightCountSec3.text != "0"))
        {

            float TopLimitPosX = ShirinaLampy / 2f + LightHelperLeftWallSection3.transform.position.x + float.Parse(LeftWallLightTopLimitSec3.text) / 100f * LightHelperLeftWallSection3.transform.localScale.x - LightHelperLeftWallSection3.transform.localScale.x / 2;
            float BotLimitPosX = -ShirinaLampy / 2f + LightHelperLeftWallSection3.transform.position.x - (1f - float.Parse(LeftWallLightBotLimitSec3.text) / 100f) * LightHelperLeftWallSection3.transform.localScale.x + LightHelperLeftWallSection3.transform.localScale.x / 2;


            float BetweenLightsDistance;
            if (RightWallLightCountSec3.text != "1")
                BetweenLightsDistance = (BotLimitPosX - TopLimitPosX) / (float.Parse(LeftWallLightCountSec3.text) - 1);
            else
                BetweenLightsDistance = 0;



            for (int i = 0; i < int.Parse(LeftWallLightCountSec3.text); i++)
            {

                GameObject LampLeftWall3Sec = GameObject.Instantiate(LampSphericToInstantiate);
                LeftWallSection3Lamps.Add(LampLeftWall3Sec);
                LampLeftWall3Sec.SetActive(true);
                if (LeftWallLightCountSec3.text != "1")
                    LampLeftWall3Sec.transform.position = new Vector3(TopLimitPosX + i * BetweenLightsDistance, LightHelperLeftWallSection3.transform.position.y, LightHelperLeftWallSection3.transform.position.z - TolschinaStenKarkasa / 2);
                else
                    LampLeftWall3Sec.transform.position = new Vector3((TopLimitPosX + BotLimitPosX) / 2f, LightHelperLeftWallSection3.transform.position.y, LightHelperLeftWallSection3.transform.position.z - TolschinaStenKarkasa / 2);
                LampLeftWall3Sec.transform.eulerAngles = new Vector3(0, -270, 0);
            }
        }


    }

    public void BuildAllMetricsLine()
    {


        foreach (GameObject go in MetricLinesGO)
        {
            GameObject.Destroy(go, 0.002f);
        }





        MetricLinesEndCones.Clear();
        MetricLinesEndCones.Capacity = 0;
        MetricLinesStartCones.Clear();
        MetricLinesStartCones.Capacity = 0;
        MetricLinesGO.Clear();
        MetricLinesGO.Capacity = 0;
       
        

        if (MetricLines==null)
            MetricLines = new List<MetricLine>();
        else
        { 
        MetricLines.Clear();
        MetricLines.Capacity = 0;
        }

        foreach (MetricLine ml in MetricLines)
        {
            GameObject.Destroy(ml.DistanceMeshTextGO, 0.03f);
        }


        AddMustHaveMetricLinesEndStartPositions();

        



        int RoundAfterZapyata = 3;
        for (int i = 0; i < MetricLines.Count; i++)
        {

            BuildMetricLine(MetricLines[i].LineStart, MetricLines[i].LineEnd, i);


            ///определяем направление измерительной линии
            // if ((Math.Round(MetricLinesEndStartPos[2 * i].x, RoundAfterZapyata) != Math.Round(MetricLinesEndStartPos[2 * i + 1].x, RoundAfterZapyata)) && (Math.Round(MetricLinesEndStartPos[2 * i].y, RoundAfterZapyata) == Math.Round(MetricLinesEndStartPos[2 * i + 1].y, RoundAfterZapyata)) && (Math.Round(MetricLinesEndStartPos[2 * i].z, RoundAfterZapyata) == Math.Round(MetricLinesEndStartPos[2 * i + 1].z, RoundAfterZapyata)))
            if (Math.Max(Math.Max(MetricLinesGO[i].GetComponent<Renderer>().bounds.size.x, MetricLinesGO[i].GetComponent<Renderer>().bounds.size.y), MetricLinesGO[i].GetComponent<Renderer>().bounds.size.z) == MetricLinesGO[i].GetComponent<Renderer>().bounds.size.x)
            {

                MetricLines[i].MetricLineDirection = MetricLine.LineDirectionsEnum.x;
                //MetricLineDirection.Add("x");
                MetricLinesGO[i].name = "MLine_X";
            }
            else
             if (Math.Max(Math.Max(MetricLinesGO[i].GetComponent<Renderer>().bounds.size.x, MetricLinesGO[i].GetComponent<Renderer>().bounds.size.y), MetricLinesGO[i].GetComponent<Renderer>().bounds.size.z) == MetricLinesGO[i].GetComponent<Renderer>().bounds.size.y)
            {
                
                MetricLines[i].MetricLineDirection = MetricLine.LineDirectionsEnum.y;
                //MetricLineDirection.Add("y");
                MetricLinesGO[i].name = "MLine_Y";
            }
            else
             if (Math.Max(Math.Max(MetricLinesGO[i].GetComponent<Renderer>().bounds.size.x, MetricLinesGO[i].GetComponent<Renderer>().bounds.size.y), MetricLinesGO[i].GetComponent<Renderer>().bounds.size.z) == MetricLinesGO[i].GetComponent<Renderer>().bounds.size.z)
            {
                MetricLines[i].MetricLineDirection = MetricLine.LineDirectionsEnum.z;
                //MetricLineDirection.Add("z");
                MetricLinesGO[i].name = "MLine_Z";
                 
            }

            ///*****определяем направление измерительной линии

            MetricLinesGO[i].name = MetricLinesGO[i].name + " " + MetricLines[i].ParentObjType.ToString();

        }

        MetricLinesTextGOReposition();

    }

    public void ChangeToggleMetricLinesShow()
    {

        {
            BuildAllMetricsLine();
            BuildMetricHolePoints();

            if (ToggleShowMetricLines.isOn)
            {
                foreach (MetricLine ml in MetricLines)
                {
                    ml.DistanceMeshTextGO.SetActive(true);

                }

                foreach (GameObject go in MetricLinesGO)
                {
                    go.SetActive(true);
                }



                foreach (GameObject go in MetricHolePointGOs)
                {
                    go.SetActive(true);
                }

                


            }
            else
            {

                foreach (MetricLine ml in MetricLines)
                {
                    ml.DistanceMeshTextGO.SetActive(false);
                }
                foreach (GameObject go in MetricLinesGO)
                {
                    go.SetActive(false);
                }



                foreach (GameObject go in MetricHolePointGOs)
                {
                    go.SetActive(false);
                }


            }
        }

    }
    public void ChangeToggleGridShow()
    {

        if (ToggleShowGrid.isOn)
        {

            Camera.main.gameObject.GetComponent<GridBuilderSecond>().enabled = true;
        }
        else
        {

            Camera.main.gameObject.GetComponent<GridBuilderSecond>().enabled = false;
        }

    }

    public void MetricLinesTextGOReposition()
    {

        if (RezhimChertezhei)
        {
            OffsetValueToDirectionFromLines = OffsetValueToDirectionLookingCameraRezhimChert;
        }

        else
        {
            OffsetValueToDirectionFromLines = OffsetValueToDirectionPerspCam1;
        }



        for (int i = 0; i < MetricLines.Count; i++)
        {

            if (MetricLines[i].MetricLineDirection == MetricLine.LineDirectionsEnum.x)
            {
                if ( MetricLines[i].VisisbleByCam==MetricLine.VisCamEnum.LeftCam)
                {
                    // MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 0, 0);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z - OffsetValueToDirectionFromLines);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_LeftCam";
                }

                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.RightCam)
                {
                    //MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 180, 0);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z + OffsetValueToDirectionFromLines);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_RightCam";
                }

                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.UpCam)
                {
                    //MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(90, -90, 0), Space.Self);
                    //  Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(90, 180, 180);


                    if (MetricLines[i].ParentObjType != MetricLine.ParentObjTypeEnum.Skam)
                        MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, BanyaHeight / 100f + OffsetValueToDirectionUpCamera, MetricLinesGO[i].transform.position.z);
                    else
                    {
                        MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y + SkamsMetricLinesYOffset, MetricLinesGO[i].transform.position.z);
                    }

                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_UpCam";







   










                }

            }

            if (MetricLines[i].MetricLineDirection == MetricLine.LineDirectionsEnum.y)
            {
                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.LeftCam)
                {
                    //  MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(90, 0, 90), Space.Self);

                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 0, 90);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z - OffsetValueToDirectionFromLines);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_LeftCam";
                }
                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.RightCam)
                {
                    //     MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(-90, 0, 90), Space.Self);
                    // Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 180, 270);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z + OffsetValueToDirectionFromLines);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_RightCam";
                }

                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.BotCam)
                {
                    // MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(180, 90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 270, 270);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x + OffsetValueToDirectionFromLines, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_BotCam";
                }

                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.TopCam)
                {
                    // MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 90, 270);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x - OffsetValueToDirectionFromLines, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_TopCam";
                }
            }

            if (MetricLines[i].MetricLineDirection == MetricLine.LineDirectionsEnum.z)
            {
                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.BotCam)
                {
                    // MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 270, 0);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x + OffsetValueToDirectionFromLines, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_BotCam";
                }
                if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.TopCam)
                {
                    //MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(0, 90, 0);
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x - OffsetValueToDirectionFromLines, MetricLinesGO[i].transform.position.y, MetricLinesGO[i].transform.position.z);
                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_TopCam";
                }
                //if ([i] == "UpCam")

                
                    if (MetricLines[i].VisisbleByCam == MetricLine.VisCamEnum.UpCam)
                    {
                    //MetricLines_DistanceMeshTextGO[i].transform.Rotate(new Vector3(90, 90, 0), Space.Self);
                    //Debug.Log(MetricLines_DistanceMeshTextGO[i].transform.eulerAngles);
                    MetricLines[i].DistanceMeshTextGO.transform.eulerAngles = new Vector3(90, 270, 0);
                    if (MetricLines[i].ParentObjType!=MetricLine.ParentObjTypeEnum.Skam)
                    MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, BanyaHeight / 100f + OffsetValueToDirectionUpCamera, MetricLinesGO[i].transform.position.z);
                    else
                    {
                        MetricLinesGO[i].transform.position = new Vector3(MetricLinesGO[i].transform.position.x, MetricLinesGO[i].transform.position.y+ SkamsMetricLinesYOffset, MetricLinesGO[i].transform.position.z);
                    }

                    MetricLinesGO[i].name = MetricLinesGO[i].name + "_UpCam";
                }
            }




        }
    }

    public void BuildMetricLine(Vector3 startPos, Vector3 endPos, int ML_index)
    {
        float cutMetricLinesAtEndAndStart = MetricLinesCone.GetComponentInChildren<Renderer>().bounds.size.z; //0.095f;

        //Debug.Log(MetricLinesCone.GetComponent<Renderer>().bounds.size.x);


        GameObject someMetricLinesGO = new GameObject();
        MetricLinesGO.Add(someMetricLinesGO);
        GameObject someML_DistanceMeshTextGO = new GameObject();
        
        //MetricLines_DistanceMeshTextGO.Add(someML_DistanceMeshTextGO);
        MetricLines[ML_index].DistanceMeshTextGO = someML_DistanceMeshTextGO;
        someML_DistanceMeshTextGO.AddComponent<TextMesh>();
       
        someML_DistanceMeshTextGO.GetComponent<TextMesh>().text = (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000)).ToString();
        // someML_DistanceMeshTextGO.GetComponent<TextMesh>().color = MetricLineTextColor;
        if ((Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) < 1500)&& (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) > 200))
            someML_DistanceMeshTextGO.GetComponent<TextMesh>().characterSize = MetricLineTextCharacterSize / 2f;
        else
        if (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) <= 200)
        {
            someML_DistanceMeshTextGO.GetComponent<TextMesh>().characterSize = MetricLineTextCharacterSize / 3f;
      
        }
        else
            someML_DistanceMeshTextGO.GetComponent<TextMesh>().characterSize = MetricLineTextCharacterSize;



        someML_DistanceMeshTextGO.GetComponent<TextMesh>().anchor = TextAnchor.LowerCenter;
        someML_DistanceMeshTextGO.GetComponent<TextMesh>().color = MetricLineTextColor;
        someML_DistanceMeshTextGO.GetComponent<TextMesh>().font = MetricLinesDistanceTextFont;
        MeshRenderer meshRendererComponent = someML_DistanceMeshTextGO.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        someML_DistanceMeshTextGO.GetComponent<Renderer>().materials = new Material[1];
        // someML_DistanceMeshTextGO.GetComponent<Renderer>().materials[0] = MetricLinesDistanceTexttMaterial;
        someML_DistanceMeshTextGO.GetComponent<Renderer>().material = MetricLinesDistanceTexttMaterial;
        //someML_DistanceMeshTextGO.GetComponent<TextMesh>().anchor=TextAnchor. 
        someMetricLinesGO.AddComponent<MeshFilter>();
        var rend = someMetricLinesGO.AddComponent<MeshRenderer>();
        rend.material = MetricLinesMaterial;


        MeshFilter meshFilter = (MeshFilter)someMetricLinesGO.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        float length = MetricLineWidth;

        float width = MetricLineWidth;
        float height = Vector3.Distance(startPos, endPos) - cutMetricLinesAtEndAndStart * 2f;


        //var elementInfo = someGO.AddComponent<WallElementInfoScript>();
        //elementInfo.length = length;
        //elementInfo.height = width;



        #region Vertices
        Vector3 p0 = new Vector3(-length * .5f, -width * .5f, height * .5f);
        Vector3 p1 = new Vector3(length * .5f, -width * .5f, height * .5f);
        Vector3 p2 = new Vector3(length * .5f, -width * .5f, -height * .5f);
        Vector3 p3 = new Vector3(-length * .5f, -width * .5f, -height * .5f);

        Vector3 p4 = new Vector3(-length * .5f, width * .5f, height * .5f);
        Vector3 p5 = new Vector3(length * .5f, width * .5f, height * .5f);
        Vector3 p6 = new Vector3(length * .5f, width * .5f, -height * .5f);
        Vector3 p7 = new Vector3(-length * .5f, width * .5f, -height * .5f);

        Vector3[] vertices = new Vector3[]
        {
	// Bottom
	p0, p1, p2, p3,
 
	// Left
	p7, p4, p0, p3,
 
	// Front
	p4, p5, p1, p0,
 
	// Back
	p6, p7, p3, p2,
 
	// Right
	p5, p6, p2, p1,
 
	// Top
	p7, p6, p5, p4
        };
        #endregion

        #region Normales
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {
	// Bottom
	down, down, down, down,
 
	// Left
	left, left, left, left,
 
	// Front
	front, front, front, front,
 
	// Back
	back, back, back, back,
 
	// Right
	right, right, right, right,
 
	// Top
	up, up, up, up
        };
        #endregion

        #region UVs
        Vector2 _00 = new Vector2(0f, 0f);
        Vector2 _10 = new Vector2(1f, 0f);
        Vector2 _01 = new Vector2(0f, 1f);
        Vector2 _11 = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
        {
	// Bottom
	_11, _01, _00, _10,
 
	// Left
	_11, _01, _00, _10,
 
	// Front
	_11, _01, _00, _10,
 
	// Back
	_11, _01, _00, _10,
 
	// Right
	_11, _01, _00, _10,
 
	// Top
	_11, _01, _00, _10,
        };
        #endregion

        #region Triangles
        int[] triangles = new int[]
        {
	// Bottom
	3, 1, 0,
    3, 2, 1,			
 
	// Left
	3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
    3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
 
	// Front
	3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
    3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
 
	// Back
	3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
    3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
 
	// Right
	3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
    3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
 
	// Top
	3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
    3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

        };
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        ;


        someML_DistanceMeshTextGO.transform.parent = someMetricLinesGO.transform;

        someMetricLinesGO.transform.position = startPos;
        someMetricLinesGO.transform.LookAt(endPos);


        //if index  то читаем листы по какой стороне и какая камера
        //  someML_DistanceMeshTextGO.transform.LookAt(Camera.main.gameObject.transform);
        //  someML_DistanceMeshTextGO.transform.Rotate(new Vector3(0, 180, 0), Space.Self);


        GameObject MetricLineConeStart = GameObject.Instantiate(MetricLinesCone);
        GameObject MetricLineConeEnd = GameObject.Instantiate(MetricLinesCone);


        if ((Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) < 1500) && (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) > 200))
        {
            MetricLineConeStart.transform.localScale = MetricLineConeStart.transform.localScale *1.3f;
            MetricLineConeEnd.transform.localScale = MetricLineConeEnd.transform.localScale * 1.3f;
        }
        else
      if (Mathf.RoundToInt(Vector3.Distance(startPos, endPos) * 1000) <= 200)
        {
            MetricLineConeStart.transform.localScale = MetricLineConeStart.transform.localScale * 0.5f;
            MetricLineConeEnd.transform.localScale = MetricLineConeEnd.transform.localScale * 0.5f;
        }
        else
        {
            MetricLineConeStart.transform.localScale = MetricLineConeStart.transform.localScale * 2f;
            MetricLineConeEnd.transform.localScale = MetricLineConeEnd.transform.localScale * 2f;
        }



        MetricLineConeEnd.SetActive(true);
        MetricLineConeStart.SetActive(true);
        MetricLinesEndCones.Add(MetricLineConeEnd);
        MetricLinesStartCones.Add(MetricLineConeStart);

        MetricLineConeStart.transform.position = startPos;
        MetricLineConeEnd.transform.position = endPos;
        MetricLineConeStart.transform.LookAt(endPos);
        MetricLineConeEnd.transform.LookAt(startPos);
        //if (MetricLineDirection   ML_index
        someMetricLinesGO.transform.position = new Vector3((endPos.x + startPos.x) / 2, (endPos.y + startPos.y) / 2f, (endPos.z + startPos.z) / 2f);
        MetricLineConeStart.transform.parent = someMetricLinesGO.transform;
        MetricLineConeEnd.transform.parent = someMetricLinesGO.transform;

    }





    public void BuildMetricHolePoints()
    {

        if (MetricHolePointGOs != null)
        {
            foreach (GameObject go in MetricHolePointGOs)
            {
                GameObject.Destroy(go, 0.002f);
            }



            MetricHolePointGOs.Clear();
            MetricHolePointGOs.Capacity = 0;

        }
        else

        {
            MetricHolePointGOs = new List<GameObject>();
        }




        if (MetricPointsEndStartPos != null)
        {
            MetricPointsEndStartPos.Clear();
            MetricPointsEndStartPos.Capacity = 0;
        }
        else
        {
            MetricPointsEndStartPos = new List<MetricHolePoint>();
        }






        // MetricPointsEndStartPos






        
        HoleScript[] HoleScriptGOs = GameObject.FindObjectsOfType<HoleScript>();

        foreach (HoleScript hs in HoleScriptGOs)
        {
            if ((hs.ParentWall==StenaDlina1)|| (hs.ParentWall == StenaDlina2)|| (hs.ParentWall == StenaShirina1_Parilka) || (hs.ParentWall == StenaShirina2_SDveryu))
            {
            GameObject someML_Point1 = new GameObject();
            someML_Point1.transform.position = hs.HoleCube1.transform.position;
            someML_Point1.name = "Metric_Point1" + "in " + hs.ParentWall.name;
            GameObject someML_Point2 = new GameObject();
            someML_Point2.transform.position = hs.HoleCube3.transform.position;
            someML_Point2.name = "Metric_Point2" + "in " + hs.ParentWall.name;
            MetricHolePoint.VisibleByCamEnum visCam;
            visCam = MetricHolePoint.VisibleByCamEnum.Left;

            if (hs.ParentWall == StenaDlina2)
                visCam = MetricHolePoint.VisibleByCamEnum.Right;
            else
             if (hs.ParentWall == StenaShirina1_Parilka)
                visCam = MetricHolePoint.VisibleByCamEnum.Top;
            else
             if (hs.ParentWall == StenaShirina2_SDveryu)
                visCam = MetricHolePoint.VisibleByCamEnum.Bot;
            string holeaxis = "z";
            if (hs.HoleAxis == "x")
            {
                holeaxis = "x";
            }
            Vector3 endOfpoint1 = new Vector3(someML_Point1.transform.position.x + ML_point_offset, someML_Point1.transform.position.y - ML_point_offset, someML_Point1.transform.position.z);
            Vector3 endOfpoint2 = new Vector3(someML_Point2.transform.position.x - ML_point_offset, someML_Point2.transform.position.y + ML_point_offset, someML_Point2.transform.position.z);
            if (holeaxis == "x")
            {
                endOfpoint1 = new Vector3(someML_Point1.transform.position.x, someML_Point1.transform.position.y - ML_point_offset, someML_Point1.transform.position.z + ML_point_offset);
                endOfpoint2 = new Vector3(someML_Point2.transform.position.x, someML_Point2.transform.position.y + ML_point_offset, someML_Point2.transform.position.z - ML_point_offset);
            }


            MetricHolePoint someMetricHolePoint1 = new MetricHolePoint(someML_Point1.transform.position, endOfpoint1, holeaxis, MetricHolePoint.HoleCubeNum.One, visCam, someML_Point1);
            MetricHolePoint someMetricHolePoint2 = new MetricHolePoint(someML_Point2.transform.position, endOfpoint2, holeaxis, MetricHolePoint.HoleCubeNum.Three, visCam, someML_Point2);


                MetricPointsEndStartPos.Add(someMetricHolePoint1);
                MetricPointsEndStartPos.Add(someMetricHolePoint2);
                MetricHolePointGOs.Add(someML_Point1);
                MetricHolePointGOs.Add(someML_Point2);















            }
        }

    }











    public void SetOptionForDoorOrProemInSec2()
    {
        if (ToggleDoorInSec2.isOn)
        {
            DoorOrProemInSec2 = "Door";



            HoleInfoInPeregorodka2 = GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>");

            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1x>", "</Cube1x>"), "<Cube1x>" + HoleAsDoorInPeregorodka2.HoleCube1.transform.position.x + "</Cube1x>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1y>", "</Cube1y>"), "<Cube1y>" + HoleAsDoorInPeregorodka2.HoleCube1.transform.position.y + "</Cube1y>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1z>", "</Cube1z>"), "<Cube1z>" + HoleAsDoorInPeregorodka2.HoleCube1.transform.position.z + "</Cube1z>");

            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3x>", "</Cube3x>"), "<Cube3x>" + HoleAsDoorInPeregorodka2.HoleCube3.transform.position.x + "</Cube3x>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3y>", "</Cube3y>"), "<Cube3y>" + HoleAsDoorInPeregorodka2.HoleCube3.transform.position.y + "</Cube3y>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3z>", "</Cube3z>"), "<Cube3z>" + HoleAsDoorInPeregorodka2.HoleCube3.transform.position.z + "</Cube3z>");

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), HoleInfoInPeregorodka2);
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DoorOrProemInSec2>", "</DoorOrProemInSec2>"), "<DoorOrProemInSec2>" + "Door" + "</DoorOrProemInSec2>");

        }
        else
        {
            DoorOrProemInSec2 = "Proem";

            HoleInfoInPeregorodka2 = GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>");

            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1x>", "</Cube1x>"), "<Cube1x>" + HoleAsProemInPeregorodka2.HoleCube1.transform.position.x + "</Cube1x>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1y>", "</Cube1y>"), "<Cube1y>" + HoleAsProemInPeregorodka2.HoleCube1.transform.position.y + "</Cube1y>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube1z>", "</Cube1z>"), "<Cube1z>" + HoleAsProemInPeregorodka2.HoleCube1.transform.position.z + "</Cube1z>");

            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3x>", "</Cube3x>"), "<Cube3x>" + HoleAsProemInPeregorodka2.HoleCube3.transform.position.x + "</Cube3x>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3y>", "</Cube3y>"), "<Cube3y>" + HoleAsProemInPeregorodka2.HoleCube3.transform.position.y + "</Cube3y>");
            HoleInfoInPeregorodka2 = HoleInfoInPeregorodka2.Replace(GetStringWithSomeStartAndEnd(HoleInfoInPeregorodka2, "<Cube3z>", "</Cube3z>"), "<Cube3z>" + HoleAsProemInPeregorodka2.HoleCube3.transform.position.z + "</Cube3z>");

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), HoleInfoInPeregorodka2);
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DoorOrProemInSec2>", "</DoorOrProemInSec2>"), "<DoorOrProemInSec2>" + "Proem" + "</DoorOrProemInSec2>");
        }

    }

    public void ManageOptionForDoorOrProemInSec2(string option)
    {
        if (option == "Door")
        {
            ToggleDoorInSec2.isOn = true;
            DoorOrProemInSec2 = "Door";
            //Peregorodka2.GetComponent<WallScript>().Holes.Clear();
            //Peregorodka2.GetComponent<WallScript>().Holes.Add(HoleAsDoorInPeregorodka2);
            HoleAsDoorInPeregorodka2.HoleConfigBtnOfThisHole.gameObject.transform.parent = CanvasGO.transform.Find("ParentForConfigBtns");
            HoleAsProemInPeregorodka2.HoleConfigBtnOfThisHole.gameObject.transform.parent = Peregorodka2.transform;

            HoleAsDoorInPeregorodka2.door3d.gameObject.SetActive(true);
            if (HoleAsDoorInPeregorodka2.DoorOpeningDirectionInfo == "")
                HoleAsDoorInPeregorodka2.DoorOpeningDirectionInfo = "IndoorLeft";
            HoleAsDoorInPeregorodka2.door3d.gameObject.SetActive(true);
            HoleAsDoorInPeregorodka2.door3d.Find("DveriWood").gameObject.SetActive(false);
            //   Debug.Log("ДВЕРИИ");
        }
        else
            if (option == "Proem")
        {
            ToggleDoorInSec2.isOn = false;
            DoorOrProemInSec2 = "Proem";
            Peregorodka2.GetComponent<WallScript>().Holes.Clear();
            Peregorodka2.GetComponent<WallScript>().Holes.Add(HoleAsProemInPeregorodka2);
            HoleAsDoorInPeregorodka2.HoleConfigBtnOfThisHole.gameObject.transform.parent = Peregorodka2.transform;
            HoleAsProemInPeregorodka2.HoleConfigBtnOfThisHole.gameObject.transform.parent = CanvasGO.transform.Find("ParentForConfigBtns");

            Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.x, ProemInSec2Height / 100f, Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.z);
            Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.x, ProemInSec2Height / 100f, Peregorodka2.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.z);
            //ProemInSec2Height
            HoleAsDoorInPeregorodka2.door3d.gameObject.SetActive(false);

        }

    }
















    public void UI_ShowToggleOfLightsInParilka()
    {

        ToggleUglovLampLeftTopInParilka.gameObject.SetActive(true);
        ToggleUglovLampRightTopInParilka.gameObject.SetActive(true);
        ToggleUglovLampBottomInParilka.gameObject.SetActive(true);
        BtnHideToggleOfLightsInParilka.gameObject.SetActive(true);
        BtnShowToggleOfLightsInParilka.gameObject.SetActive(false);

    }


    public void UI_HideToggleOfLightsInParilka()
    {

        ToggleUglovLampLeftTopInParilka.gameObject.SetActive(false);
        ToggleUglovLampRightTopInParilka.gameObject.SetActive(false);
        ToggleUglovLampBottomInParilka.gameObject.SetActive(false);
        BtnHideToggleOfLightsInParilka.gameObject.SetActive(false);
        BtnShowToggleOfLightsInParilka.gameObject.SetActive(true);
    }














    public void RefreshLightsConfigPanel()
    {

        OnConfigLightInSec23ChangeDropdownManageUI();
        //if (PanelLightConfigInSection2_3.activeSelf)
        //{
        //    PanelLightConfigInSection2_3.SetActive(false);
        //    BtnLightCinfigPanelOpener.onClick.Invoke();
        //}
    }



    public void UIManageHideSlivePos4and5IfNo3Section()
    {
      //  SlivPosSoglasovaniePanel.SetActive(true);
        if (UIDropdownForSectionCount.value==0)
        {
            ImageSchemeSlivPos3.gameObject.SetActive(true);
            ImageSchemeSlivPos5.gameObject.SetActive(false);
            ToggleSlivePos1.gameObject.SetActive(true);
            ToggleSlivePos2.gameObject.SetActive(true);
            ToggleSlivePos3.gameObject.SetActive(true);
            ToggleSlivePos4.gameObject.SetActive(false);
            ToggleSlivePos5.gameObject.SetActive(false);

        }
        else
        {
            if (UIDropdownForSectionCount.value == 1)
            {
                ImageSchemeSlivPos3.gameObject.SetActive(false);
                ImageSchemeSlivPos5.gameObject.SetActive(true);
                ToggleSlivePos1.gameObject.SetActive(true);
                ToggleSlivePos2.gameObject.SetActive(true);
                ToggleSlivePos3.gameObject.SetActive(true);
                ToggleSlivePos4.gameObject.SetActive(true);
                ToggleSlivePos5.gameObject.SetActive(true);
            }
        }
    }

 

   
    public string GetStringBetweenStrings(string inputString, string _firstString, string _secondString)
    {

        if (inputString.Contains(_firstString) && inputString.Contains(_secondString))
        {
            string stringAfterFirstString = inputString.Remove(0, inputString.IndexOf(_firstString) + _firstString.Length);




            string stringAfterSecondString = stringAfterFirstString.Substring(stringAfterFirstString.IndexOf(_secondString));



            //inputString = inputString.
            inputString = inputString.Substring(inputString.IndexOf(_firstString) + _firstString.Length, stringAfterFirstString.Length - stringAfterSecondString.Length);




            return inputString;
        }
        else

        {

            return "";

        }


    }
    public void UIManageForAddDeleteOptions()
    {
        if (DropdownSelectWallForDeleteAdd.value==1)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }


        if (DropdownSelectWallForDeleteAdd.value == 2)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }


        if (DropdownSelectWallForDeleteAdd.value == 3)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(false);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }


        if (DropdownSelectWallForDeleteAdd.value == 4)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(false);
        }

        if (DropdownSelectWallForDeleteAdd.value == 5)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }

        if (DropdownSelectWallForDeleteAdd.value == 6)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }

        if (DropdownSelectWallForDeleteAdd.value == 0)
        {
            ToggleHoleAsDoorAdd.gameObject.SetActive(true);
            ToggleHoleAsWindowAdd.gameObject.SetActive(true);
            ToggleHoleAsOtdushinaAdd.gameObject.SetActive(true);
        }

    }


 


    public void SetOptionForHolesByAdding()
    {
        
            if (DropdownSelectWallForDeleteAdd.value==1)
        {
           
            {
                if (ToggleHoleAsWindowAdd.isOn) 
                {
                    string HolesInfoInWall1 = GetStringBetweenStrings(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>");
                   
                    int Wall1_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>")); 
                    HolesInfoInWall1 = HolesInfoInWall1.Replace("<HolesCount>"+ GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>")+ "</HolesCount>", "<HolesCount>" + (Wall1_HolesCount+1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowZGOToInstanciate);
                    
                    newHole.transform.parent = StenaDlina1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall= StenaDlina1;
                    newHole.SetActive(true);
                    StenaDlina1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall1 = HolesInfoInWall1 + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>"               + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>"; 
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + HolesInfoInWall1 + "</Wall1Holes>");



 




                }

                if (ToggleHoleAsDoorAdd.isOn) 
                {
                    string HolesInfoInWall1 = GetStringBetweenStrings(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>");
                    
                    int Wall1_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall1 = HolesInfoInWall1.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall1_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorZGOToInstanciate);
                    
                    newHole.transform.parent = StenaDlina1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina1;
                    newHole.SetActive(true);
                    StenaDlina1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall1 = HolesInfoInWall1 + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + HolesInfoInWall1 + "</Wall1Holes>");



 




                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {

                    string HolesInfoInWall1 = GetStringBetweenStrings(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>");

                    int Wall1_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall1 = HolesInfoInWall1.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall1_HolesCount + 1) + "</HolesCount>");
                    
                    //public GameObject OtdusinaXGOToInstanciate;

                    GameObject newHole = GameObject.Instantiate(OtdushinaZGOToInstanciate);
                    
                    newHole.transform.parent = StenaDlina1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina1;
                    newHole.SetActive(true);
                    StenaDlina1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall1 = HolesInfoInWall1 + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + HolesInfoInWall1 + "</Wall1Holes>");




 



                }

                
            }

        }


        if (DropdownSelectWallForDeleteAdd.value == 2)
        {
            //string  HolesInfoInWall2 = GetStringBetweenStrings(this.GetComponent<BanyaGeneralInfoScript>().CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>");


            {
                if (ToggleHoleAsWindowAdd.isOn)
                {
                    string HolesInfoInWall2 = GetStringBetweenStrings(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>");

                    int Wall2_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall2 = HolesInfoInWall2.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall2_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowZGOToInstanciate);
                    
                    newHole.transform.parent = StenaDlina2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina2;
                    newHole.SetActive(true);
                    StenaDlina2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall2 = HolesInfoInWall2 + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>"), "<Wall2Holes>" + HolesInfoInWall2 + "</Wall2Holes>");

 

                }



                if (ToggleHoleAsDoorAdd.isOn)
                {
                    string HolesInfoInWall2 = GetStringBetweenStrings(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>");

                    int Wall2_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall2 = HolesInfoInWall2.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall2_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorZGOToInstanciate);
                    newHole.transform.parent = StenaDlina2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina2;
                    newHole.SetActive(true);
                    StenaDlina2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall2 = HolesInfoInWall2 + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>"), "<Wall2Holes>" + HolesInfoInWall2 + "</Wall2Holes>");


 



                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {
                    string HolesInfoInWall2 = GetStringBetweenStrings(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>");

                    int Wall2_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall2 = HolesInfoInWall2.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall2, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall2_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(OtdushinaZGOToInstanciate);
                    newHole.transform.parent = StenaDlina2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina2;
                    newHole.SetActive(true);
                    StenaDlina2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall2 = HolesInfoInWall2 + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall2_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>"), "<Wall2Holes>" + HolesInfoInWall2 + "</Wall2Holes>");


 
                }


            }

        }


        if (DropdownSelectWallForDeleteAdd.value == 3)   //стена по ширине парилки
        {
           // string HolesInfoInWall3 = GetStringBetweenStrings(this.GetComponent<BanyaGeneralInfoScript>().CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>");
            {
                if (ToggleHoleAsWindowAdd.isOn)
                {





                    string HolesInfoInWall3 = GetStringBetweenStrings(CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>");
                    int Wall3_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall3, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall3 = HolesInfoInWall3.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall3, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall3_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowXGOToInstanciate);
                    newHole.transform.parent = StenaShirina1_Parilka.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaShirina1_Parilka;
                    newHole.SetActive(true);
                    StenaShirina1_Parilka.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());

                    //print("newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z: " + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z);
                    //в середину стены 
                    //print("BanyaWidth"+BanyaWidth);
                    //newHole.GetComponentInChildren<HoleScript>().HoleLine1.transform.position = new Vector3(newHole.GetComponentInChildren<HoleScript>().HoleLine1.transform.position.x, newHole.GetComponentInChildren<HoleScript>().HoleLine1.transform.position.y, BanyaWidth / 100f / 2f);
                    //newHole.GetComponentInChildren<HoleScript>().HoleLine3.transform.position = new Vector3(newHole.GetComponentInChildren<HoleScript>().HoleLine3.transform.position.x, newHole.GetComponentInChildren<HoleScript>().HoleLine3.transform.position.y,  BanyaWidth / 100f / 2f);
                    //newHole.GetComponentInChildren<HoleScript>().HoleLine2.transform.position = new Vector3(newHole.GetComponentInChildren<HoleScript>().HoleLine2.transform.position.x, newHole.GetComponentInChildren<HoleScript>().HoleLine2.transform.position.y, BanyaWidth / 100f / 2f - (newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z- newHole.GetComponentInChildren<HoleScript>().HoleCube2.transform.position.z)/2f);
                    //newHole.GetComponentInChildren<HoleScript>().HoleLine4.transform.position = new Vector3(newHole.GetComponentInChildren<HoleScript>().HoleLine4.transform.position.x, newHole.GetComponentInChildren<HoleScript>().HoleLine4.transform.position.y, BanyaWidth / 100f / 2f + (newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z - newHole.GetComponentInChildren<HoleScript>().HoleCube2.transform.position.z) / 2f);

             
                    //newHole.GetComponentInChildren<HoleScript>().Update();
                    //newHole.GetComponentInChildren<HoleScript>().RefreshHoles();
                    //в середину стены 

                    
                    HolesInfoInWall3 = HolesInfoInWall3 + "<Hole_" + (Wall3_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall3_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall3_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall3_HolesCount + 1) + "_Cube3Pos>";

                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>"), "<Wall3Holes>" + HolesInfoInWall3 + "</Wall3Holes>");


 
                }



                if (ToggleHoleAsDoorAdd.isOn)
                {
                    /*
                    string HolesInfoInWall1 = GetStringBetweenStrings(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>");

                    int Wall1_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall1 = HolesInfoInWall1.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall1, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall1_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorZGOToInstanciate);
                    newHole.transform.parent = StenaDlina1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaDlina1;
                    newHole.SetActive(true);
                    StenaDlina1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall1 = HolesInfoInWall1 + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponent<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponent<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponent<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponent<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponent<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponent<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall1_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + HolesInfoInWall1 + "</Wall1Holes>");
                    */
                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {
                    string HolesInfoInWall3 = GetStringBetweenStrings(CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>");

                    int Wall3_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall3, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall3 = HolesInfoInWall3.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall3, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall3_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate( OtdusinaXGOToInstanciate);
                    newHole.transform.parent = StenaShirina1_Parilka.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaShirina1_Parilka;
                    newHole.SetActive(true);
                    StenaShirina1_Parilka.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall3 = HolesInfoInWall3 + "<Hole_" + (Wall3_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall3_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall3_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall3_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>"), "<Wall3Holes>" + HolesInfoInWall3 + "</Wall3Holes>");

 
                     
                }


            }
        }


        if (DropdownSelectWallForDeleteAdd.value == 4)
        {
          
            {
                if (ToggleHoleAsWindowAdd.isOn)
                {
                    string HolesInfoInWall4 = GetStringBetweenStrings(CodeOfBanya, "<Wall4Holes>", "</Wall4Holes>");

                    int Wall4_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall4, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall4 = HolesInfoInWall4.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall4, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall4_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowXGOToInstanciate);
                    newHole.transform.parent = StenaShirina2_SDveryu.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaShirina2_SDveryu;
                    newHole.SetActive(true);
                    StenaShirina2_SDveryu.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall4 = HolesInfoInWall4 + "<Hole_" + (Wall4_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall4_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall4_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall4_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall4Holes>", "</Wall4Holes>"), "<Wall4Holes>" + HolesInfoInWall4 + "</Wall4Holes>");


                

                }



                if (ToggleHoleAsDoorAdd.isOn)
                {
                    string HolesInfoInWall4 = GetStringBetweenStrings(CodeOfBanya, "<Wall4Holes>", "</Wall4Holes>");

                    int Wall4_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall4, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall4 = HolesInfoInWall4.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall4, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall4_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorXGOToInstanciate);
                    newHole.transform.parent = StenaShirina2_SDveryu.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = StenaShirina2_SDveryu;
                    newHole.SetActive(true);
                    StenaShirina2_SDveryu.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall4 = HolesInfoInWall4 + "<Hole_" + (Wall4_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall4_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall4_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall4_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall4Holes>", "</Wall4Holes>"), "<Wall4Holes>" + HolesInfoInWall4 + "</Wall4Holes>");


 

                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {

                }


            }
        }


        if (DropdownSelectWallForDeleteAdd.value == 5)
        {
        
            {
                if (ToggleHoleAsWindowAdd.isOn)
                {
                    string HolesInfoInWall5 = GetStringBetweenStrings(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>");

                    int Wall5_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall5 = HolesInfoInWall5.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall5_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowXGOToInstanciate);
                    newHole.transform.parent = Peregorodka1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka1;
                    newHole.SetActive(true);
                    Peregorodka1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall5 = HolesInfoInWall5 + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>"), "<Wall5Holes>" + HolesInfoInWall5 + "</Wall5Holes>");


 

                }



                if (ToggleHoleAsDoorAdd.isOn)
                {
                    string HolesInfoInWall5 = GetStringBetweenStrings(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>");

                    int Wall5_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall5 = HolesInfoInWall5.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall5_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorXGOToInstanciate);
                    newHole.transform.parent = Peregorodka1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka1;
                    newHole.SetActive(true);
                    Peregorodka1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall5 = HolesInfoInWall5 + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>"), "<Wall5Holes>" + HolesInfoInWall5 + "</Wall5Holes>");

 

                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {
                    string HolesInfoInWall5 = GetStringBetweenStrings(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>");

                    int Wall5_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall5 = HolesInfoInWall5.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall5, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall5_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(ProemGOToInstanciate);
                    newHole.transform.parent = Peregorodka1.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka1;
                    newHole.SetActive(true);
                    Peregorodka1.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall5 = HolesInfoInWall5 + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall5_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>"), "<Wall5Holes>" + HolesInfoInWall5 + "</Wall5Holes>");

 

                }


            }
        }


        if (DropdownSelectWallForDeleteAdd.value == 6)
        {
          
            {
                if (ToggleHoleAsWindowAdd.isOn)
                {
                    string HolesInfoInWall6 = GetStringBetweenStrings(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>");

                    int Wall6_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall6 = HolesInfoInWall6.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall6_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(WindowXGOToInstanciate);
                    newHole.transform.parent = Peregorodka2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka2;
                    newHole.SetActive(true);
                    Peregorodka2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall6 = HolesInfoInWall6 + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), "<Wall6Holes>" + HolesInfoInWall6 + "</Wall6Holes>");

 
                }



                if (ToggleHoleAsDoorAdd.isOn)
                {
                    string HolesInfoInWall6 = GetStringBetweenStrings(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>");

                    int Wall6_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall6 = HolesInfoInWall6.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall6_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(DoorXGOToInstanciate);
                    newHole.transform.parent = Peregorodka2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka2;
                    newHole.SetActive(true);
                    Peregorodka2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall6 = HolesInfoInWall6 + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), "<Wall6Holes>" + HolesInfoInWall6 + "</Wall6Holes>");




                


                }


                if (ToggleHoleAsOtdushinaAdd.isOn)
                {
                    string HolesInfoInWall6 = GetStringBetweenStrings(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>");

                    int Wall6_HolesCount = int.Parse(GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>"));
                    HolesInfoInWall6 = HolesInfoInWall6.Replace("<HolesCount>" + GetStringBetweenStrings(HolesInfoInWall6, "<HolesCount>", "</HolesCount>") + "</HolesCount>", "<HolesCount>" + (Wall6_HolesCount + 1) + "</HolesCount>");
                    GameObject newHole = GameObject.Instantiate(ProemGOToInstanciate);
                    newHole.transform.parent = Peregorodka2.transform;
                    newHole.GetComponentInChildren<HoleScript>().ParentWall = Peregorodka2;
                    newHole.SetActive(true);
                    Peregorodka2.GetComponent<WallScript>().Holes.Add(newHole.GetComponentInChildren<HoleScript>());
                    HolesInfoInWall6 = HolesInfoInWall6 + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Cube1x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.x + "</Cube1x>" + "<Cube1y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.y + "</Cube1y>" + "<Cube1z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube1.transform.position.z + "</Cube1z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube1Pos>" + "<Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>" + "<Cube3x>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.x + "</Cube3x>" + "<Cube3y>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.y + "</Cube3y>" + "<Cube3z>" + newHole.GetComponentInChildren<HoleScript>().HoleCube3.transform.position.z + "</Cube3z>" + "</Hole_" + (Wall6_HolesCount + 1) + "_Cube3Pos>";
                    CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), "<Wall6Holes>" + HolesInfoInWall6 + "</Wall6Holes>");
                }


            
        }

        }


 




    }



    /*
    public void ManageSetOptionForPechOutdoorSkam1(string option)
    {
        if (option == "Disabled")
        {
            SmallSkam1.SetActive(false);
            Debug.Log("Skam1 disabled");
        }
        else
            if (option == "EnabledVertical")
        {
            SmallSkam1.SetActive(true);
            SmallSkam1.transform.eulerAngles = new Vector3(0, 0, 0);
            SmallSkam1.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x - DlinaMaloiSkam, SmallSkam1.transform.position.y, TolschinaStenKarkasa);

        }
        else
        {
            if (option == "EnabledHorizontal")
            {
                SmallSkam1.transform.eulerAngles = new Vector3(0, 270, 0);
                SmallSkam1.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x, SmallSkam1.transform.position.y, TolschinaStenKarkasa);
                SmallSkam1.SetActive(true);
            }
        }

    }

    */























    public void SetOptionForUglovLampLeftTopInParilka()   //LeftTop Ugol
    {
        if (ToggleUglovLampLeftTopInParilka.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampLeftTopInParilka>", "</UglovLampLeftTopInParilka>"), "<UglovLampLeftTopInParilka>" + "enabled" + "</UglovLampLeftTopInParilka>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampLeftTopInParilka>", "</UglovLampLeftTopInParilka>"), "<UglovLampLeftTopInParilka>" + "disabled" + "</UglovLampLeftTopInParilka>");
        }
     }
    public void ManageOptionForUglovLampLeftTopInParilka(string option)
    {
        if (option == "enabled")

        {
            UglovLampLeftTopInParilka.SetActive(true);
            ToggleUglovLampLeftTopInParilka.isOn = true;
        }
        else
            if (option == "disabled")

        {
            UglovLampLeftTopInParilka.SetActive(false);
            ToggleUglovLampLeftTopInParilka.isOn = false;
        }
    }




    public void SetOptionForUglovLampRightTopInParilka()   //LeftTop Ugol
    {
        if (ToggleUglovLampRightTopInParilka.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampRightTopInParilka>", "</UglovLampRightTopInParilka>"), "<UglovLampRightTopInParilka>" + "enabled" + "</UglovLampRightTopInParilka>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampRightTopInParilka>", "</UglovLampRightTopInParilka>"), "<UglovLampRightTopInParilka>" + "disabled" + "</UglovLampRightTopInParilka>");
        }
    }
    public void ManageOptionForUglovLampRightTopInParilka(string option)
    {
        if (option == "enabled")

        {
            UglovLampRightTopInParilka.SetActive(true);
            ToggleUglovLampRightTopInParilka.isOn = true;
        }
        else
            if (option == "disabled")

        {
            UglovLampRightTopInParilka.SetActive(false);
            ToggleUglovLampRightTopInParilka.isOn = false;
        }
    }






    public void SetOptionForUglovLampBottomInParilka()   //LeftTop Ugol
    {
        if (ToggleUglovLampBottomInParilka.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampBottomInParilka>", "</UglovLampBottomInParilka>"), "<UglovLampBottomInParilka>" + "enabled" + "</UglovLampBottomInParilka>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<UglovLampBottomInParilka>", "</UglovLampBottomInParilka>"), "<UglovLampBottomInParilka>" + "disabled" + "</UglovLampBottomInParilka>");
        }
    }
    public void ManageOptionForUglovLampBottomInParilka(string option)
    {
        if (option == "enabled")

        {
            UglovLampBottomInParilka.SetActive(true);
            ToggleUglovLampBottomInParilka.isOn = true;
        }
        else
            if (option == "disabled")

        {
            UglovLampBottomInParilka.SetActive(false);
            ToggleUglovLampBottomInParilka.isOn = false;
        }
    }








    #region UISolved
    public void SetOptionForPechOutdoorSkam1()
    {
        if ((ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn) && (!ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam1>", "</PechOutdoorSkam1>"), "<PechOutdoorSkam1>" + "EnabledHorizontal" + "</PechOutdoorSkam1>");
        }

        if ((!ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn) && (ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam1>", "</PechOutdoorSkam1>"), "<PechOutdoorSkam1>" + "EnabledVertical" + "</PechOutdoorSkam1>");
        }

        if ((!ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn) && (!ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam1>", "</PechOutdoorSkam1>"), "<PechOutdoorSkam1>" + "Disabled" + "</PechOutdoorSkam1>");
        }
    }
    public void ManageSetOptionForPechOutdoorSkam1(string option)
    {
        if (option == "Disabled")
        {
            SmallSkam1.SetActive(false);
            
            ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn = false;
        }
        else
            if (option == "EnabledVertical")
        {
            ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn = true;
            SmallSkam1.SetActive(true);
            SmallSkam1.transform.eulerAngles = new Vector3(0, 0, 0);
            SmallSkam1.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x - DlinaMaloiSkam, SmallSkam1.transform.position.y, TolschinaStenKarkasa);

        }
        else
        {
            if (option == "EnabledHorizontal")
            {
                ToggleSelectHorLeftSkamNizOutdoorPech3Section.isOn = true;
                ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.isOn = false;
                SmallSkam1.transform.eulerAngles = new Vector3(0, 270, 0);
                SmallSkam1.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x, SmallSkam1.transform.position.y, TolschinaStenKarkasa);
                SmallSkam1.SetActive(true);
            }
        }

    }

    public void SetOptionForPechOutdoorSkam2()
    {
        if ((ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn) && (!ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam2>", "</PechOutdoorSkam2>"), "<PechOutdoorSkam2>" + "EnabledHorizontal" + "</PechOutdoorSkam2>");
        }

        if ((!ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn) && (ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam2>", "</PechOutdoorSkam2>"), "<PechOutdoorSkam2>" + "EnabledVertical" + "</PechOutdoorSkam2>");
        }

        if ((!ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn) && (!ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam2>", "</PechOutdoorSkam2>"), "<PechOutdoorSkam2>" + "Disabled" + "</PechOutdoorSkam2>");
        }
    }
    public void ManageSetOptionForPechOutdoorSkam2(string option)
    {
        if (option == "Disabled")
        {
            SmallSkam2.SetActive(false);
            ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn = false;

        }
        else
            if (option == "EnabledVertical")
        {
            SmallSkam2.SetActive(true);
            SmallSkam2.transform.eulerAngles = new Vector3(0, 0, 0);
            SmallSkam2.transform.position = new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + TolschinaStenKarkasa / 2, SmallSkam2.transform.position.y, TolschinaStenKarkasa);
            ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn = true;
        }
        else
        {
            if (option == "EnabledHorizontal")
            {
                SmallSkam2.transform.eulerAngles = new Vector3(0, 90, 0);
                SmallSkam2.transform.position = new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + TolschinaStenKarkasa / 2, SmallSkam1.transform.position.y, DlinaMaloiSkam + TolschinaStenKarkasa / 2);
                SmallSkam2.SetActive(true);
                ToggleSelectHorLeftSkamVerhOutdoorPech3Section.isOn = true;
                ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.isOn = false;
            }
        }

    }


    public void SetOptionForPechOutdoorSkam3()
    {
        if ((ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn) && (!ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam3>", "</PechOutdoorSkam3>"), "<PechOutdoorSkam3>" + "EnabledHorizontal" + "</PechOutdoorSkam3>");
        }

        if ((!ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn) && (ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam3>", "</PechOutdoorSkam3>"), "<PechOutdoorSkam3>" + "EnabledVertical" + "</PechOutdoorSkam3>");
        }

        if ((!ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn) && (!ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam3>", "</PechOutdoorSkam3>"), "<PechOutdoorSkam3>" + "Disabled" + "</PechOutdoorSkam3>");
        }
    }
    public void ManageSetOptionForPechOutdoorSkam3(string option)
    {
        if (option == "Disabled")
        {
            SmallSkam3.SetActive(false);
            ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn = false;

        }
        else
            if (option == "EnabledVertical")
        {
            SmallSkam3.SetActive(true);
            SmallSkam3.transform.eulerAngles = new Vector3(0, 180, 0);
            SmallSkam3.transform.position = new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + DlinaMaloiSkam, SmallSkam3.transform.position.y, BanyaWidth / 100 - TolschinaStenKarkasa);
            ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn = true;
        }
        else
        {
            if (option == "EnabledHorizontal")
            {
                SmallSkam3.transform.eulerAngles = new Vector3(0, 90, 0);
                SmallSkam3.transform.position = new Vector3(Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x + TolschinaStenKarkasa / 2, SmallSkam3.transform.position.y, BanyaWidth / 100 - TolschinaStenKarkasa);
                SmallSkam3.SetActive(true);
                ToggleSelectHorRightSkamVerhOutdoorPech3Section.isOn = true;
                ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.isOn = false;
            }
        }

    }

    public void SetOptionForPechOutdoorSkam4()
    {
        if ((ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn) && (!ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam4>", "</PechOutdoorSkam4>"), "<PechOutdoorSkam4>" + "EnabledHorizontal" + "</PechOutdoorSkam4>");
        }

        if ((!ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn) && (ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam4>", "</PechOutdoorSkam4>"), "<PechOutdoorSkam4>" + "EnabledVertical" + "</PechOutdoorSkam4>");
        }

        if ((!ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn) && (!ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn))
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechOutdoorSkam4>", "</PechOutdoorSkam4>"), "<PechOutdoorSkam4>" + "Disabled" + "</PechOutdoorSkam4>");
        }
    }
    public void ManageSetOptionForPechOutdoorSkam4(string option)
    {
        if (option == "Disabled")
        {
            SmallSkam4.SetActive(false);
            ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn = false;
        }
        else
            if (option == "EnabledVertical")
        {
            SmallSkam4.SetActive(true);
            SmallSkam4.transform.eulerAngles = new Vector3(0, 180, 0);
            SmallSkam4.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x - TolschinaStenKarkasa / 2, SmallSkam3.transform.position.y, BanyaWidth / 100 - TolschinaStenKarkasa);
            ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn = false;
            ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn = true;
        }
        else
        {
            if (option == "EnabledHorizontal")
            {
                ToggleSelectHorRightSkamNizOutdoorPech3Section.isOn = true;
                ToggleSelectVerticalRightSkamNizOutdoorPech3Section.isOn = false;
                SmallSkam4.transform.eulerAngles = new Vector3(0, 270, 0);
                SmallSkam4.transform.position = new Vector3(Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x - TolschinaStenKarkasa / 2, SmallSkam3.transform.position.y, BanyaWidth / 100 - DlinaMaloiSkam - TolschinaStenKarkasa / 2);
                SmallSkam4.SetActive(true);
            }
        }

    }

    public void SetOptionForPechIndoorLeftBigSkam()
    {
        if (ToggleSelectLeftSkamIndoorPech3Section.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorLeftBigSkam>", "</PechIndoorLeftBigSkam>"), "<PechIndoorLeftBigSkam>" + "Enabled" + "</PechIndoorLeftBigSkam>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorLeftBigSkam>", "</PechIndoorLeftBigSkam>"), "<PechIndoorLeftBigSkam>" + "Disabled" + "</PechIndoorLeftBigSkam>");
        }

    }
    public void ManageOptionForPechIndoorLeftBigSkam(string option)
    {

        if (option == "Disabled")
        {
            BigSkamLeft.SetActive(false);
            ToggleSelectLeftSkamIndoorPech3Section.isOn = false;

        }
        else
            if (option == "Enabled")
        {
            BigSkamLeft.SetActive(true);
            BigSkamLeft.transform.eulerAngles = new Vector3(0, 0, 0);
            BigSkamLeft.transform.position = new Vector3((Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2 - DlinaBigSkam / 2, BigSkamLeft.transform.position.y, TolschinaStenKarkasa);
            ToggleSelectLeftSkamIndoorPech3Section.isOn = true;
        }


    }

    public void SetOptionForPechIndoorRightBigSkam()
    {
        if (ToggleSelectRightSkamIndoorPech3Section.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorRightBigSkam>", "</PechIndoorRightBigSkam>"), "<PechIndoorRightBigSkam>" + "Enabled" + "</PechIndoorRightBigSkam>");
        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorRightBigSkam>", "</PechIndoorRightBigSkam>"), "<PechIndoorRightBigSkam>" + "Disabled" + "</PechIndoorRightBigSkam>");
        }

    }
    public void ManageOptionForPechIndoorRightBigSkam(string option)
    {
        if (option == "Disabled")
        {
            BigSkamRight.SetActive(false);
            ToggleSelectRightSkamIndoorPech3Section.isOn = false;
        }
        else
            if (option == "Enabled")
        {
            BigSkamRight.SetActive(true);
            BigSkamRight.transform.eulerAngles = new Vector3(0, 180, 0);
            BigSkamRight.transform.position = new Vector3((Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2 + DlinaBigSkam / 2, BigSkamRight.transform.position.y, BanyaWidth / 100 - TolschinaStenKarkasa);
            ToggleSelectRightSkamIndoorPech3Section.isOn = true;
        }

    }
    public void SetOptionForDushWaterPositionPercent()
    {

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushWaterWallPercent>", "</DushWaterWallPercent>"), "<DushWaterWallPercent>" + SliderDushWaterWallPercent.value + "</DushWaterWallPercent>");

    }
    public void ManageOptionForDushWaterPositionPercent(int PercentOption, string DushWaterPos)
    {
        if (DushWaterPos == "1")
        {
            Debug.Log("asdfasdfasdfasdf");
            DushWaterPos1.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x, DushWaterPos1.transform.position.y, (StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z + StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z) / (1 / ((float)PercentOption / 100)));
            //DushWaterPos1.transform.GetChild(1).transform.rotation.SetLookRotation(Camera.main.gameObject.transform.position);
        }

        //(1 / ((float)PercentOption / 100))
        if (DushWaterPos == "2")
        {
            DushWaterPos2.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - (StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / (1 / (1 - (float)PercentOption / 100)), DushWaterPos2.transform.position.y, StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z);
            
        }

        if (DushWaterPos == "3")
        {
            DushWaterPos3.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - (StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / (1 / (1 - (float)PercentOption / 100)), DushWaterPos3.transform.position.y, StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z);
            
        }

        SliderDushWaterWallPercent.value = PercentOption;
    }

    public void SetOptionForDushWaterPosition()
    {
        if (ToggleDushWaterWall1.isOn)
        {


            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushWaterWall>", "</DushWaterWall>"), "<DushWaterWall>" + "1" + "</DushWaterWall>");

        }

        if (ToggleDushWaterWall2.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushWaterWall>", "</DushWaterWall>"), "<DushWaterWall>" + "2" + "</DushWaterWall>");
        }

        if (ToggleDushWaterWall3.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushWaterWall>", "</DushWaterWall>"), "<DushWaterWall>" + "3" + "</DushWaterWall>");
        }


    }
    public void ManageOptionForDushWaterPosition(string option)
    {

        if (option == "1")
        {


            DushWaterPos1.SetActive(true);
            DushWaterPos1.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x, DushWaterPos1.transform.position.y, (StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z + StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z) / 2);
            DushWaterPos2.SetActive(false);
            DushWaterPos3.SetActive(false);
            ToggleDushWaterWall1.isOn = true;
            ToggleDushWaterWall2.isOn = false;
            ToggleDushWaterWall3.isOn = false;

        }
        if (option == "2")
        {

            DushWaterPos1.SetActive(false);
            DushWaterPos2.SetActive(true);
            DushWaterPos2.transform.position = new Vector3((StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, DushWaterPos2.transform.position.y, StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z);
            DushWaterPos3.SetActive(false);
            ToggleDushWaterWall1.isOn = false;
            ToggleDushWaterWall2.isOn = true;
            ToggleDushWaterWall3.isOn = false;


        }
        if (option == "3")
        {
            DushWaterPos1.SetActive(false);
            DushWaterPos2.SetActive(false);
            DushWaterPos3.SetActive(true);
            DushWaterPos3.transform.position = new Vector3((StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, DushWaterPos3.transform.position.y, StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z);
            ToggleDushWaterWall1.isOn = false;
            ToggleDushWaterWall2.isOn = false;
            ToggleDushWaterWall3.isOn = true;

        }



    }

    public void SetTolschinaStenKarkasa()
    {
        TolschinaStenKarkasa = float.Parse(TolschinaStenKarkasaIF.text) / 100f;
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<TolschinaSten>", "</TolschinaSten>"), "<TolschinaSten>" + float.Parse(TolschinaStenKarkasaIF.text) / 100f + "</TolschinaSten>");


    }


    public void SetOptionForObjectsVisibility()
    {
        if (TogglePechVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechVisible>", "</PechVisible>"), "<PechVisible>" + "Yes" + "</PechVisible>");

        }
        else
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechVisible>", "</PechVisible>"), "<PechVisible>" + "No" + "</PechVisible>");
        }





        if (ToggleStolsAndSkamsVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<StolsAndSkamsVisible>", "</StolsAndSkamsVisible>"), "<StolsAndSkamsVisible>" + "Yes" + "</StolsAndSkamsVisible>");

        }
        else
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<StolsAndSkamsVisible>", "</StolsAndSkamsVisible>"), "<StolsAndSkamsVisible>" + "No" + "</StolsAndSkamsVisible>");
        }





        if (ToggleLampsVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LampsVisible>", "</LampsVisible>"), "<LampsVisible>" + "Yes" + "</LampsVisible>");

        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<LampsVisible>", "</LampsVisible>"), "<LampsVisible>" + "No" + "</LampsVisible>");
        }





        if (ToggleSidushkiVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SidushkiVisible>", "</SidushkiVisible>"), "<SidushkiVisible>" + "Yes" + "</SidushkiVisible>");

        }
        else
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SidushkiVisible>", "</SidushkiVisible>"), "<SidushkiVisible>" + "No" + "</SidushkiVisible>");
        }










        if (ToggleDveriVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "DveriVisible>", "</DveriVisible>"), "<DveriVisible>" + "Yes" + "</DveriVisible>");

        }
        else
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DveriVisible>", "</DveriVisible>"), "<DveriVisible>" + "No" + "</DveriVisible>");
        }





    }
    public void ManageVisibilityOptionForObjects(string PechVisibilityOption, string StolsAndSkamsVisibilityOption, string LampsVisibilityOption, string SidushkiVisibilityOption, string DveriVisibilityOption)
    {
        if (PechVisibilityOption == "Yes")
        {

            Pech.transform.localScale = new Vector3(1, 1, 1);
            TogglePechVisibility.isOn = true;
        }
        else if (PechVisibilityOption == "No")
        {

            Pech.transform.localScale = new Vector3(0, 0, 0);
            TogglePechVisibility.isOn = false;
        }

         


        if (StolsAndSkamsVisibilityOption == "Yes")
        {

            for (int i = 0; i < StolsAndSkams.Length; i++)
            {

             

             


                if (StolsAndSkams[i].GetComponent<SkamScript>() != null)
                {
                    foreach (Renderer r in StolsAndSkams[i].GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = true;
                    }
                }
                else
                {
                    StolsAndSkams[i].transform.localScale = new Vector3(1, 1, 1);
                }
            }
            ToggleStolsAndSkamsVisibility.isOn = true;

        }
        else if (StolsAndSkamsVisibilityOption == "No")
        {

            for (int i = 0; i < StolsAndSkams.Length; i++)
            {
                if (StolsAndSkams[i].GetComponent<SkamScript>() != null)
                {
                    foreach (Renderer r in StolsAndSkams[i].GetComponentsInChildren<Renderer>())
                    {
                        r.enabled = false;
                    }
                }
                else
                { 
                StolsAndSkams[i].transform.localScale = new Vector3(0, 0, 0);
                }


            }
            ToggleStolsAndSkamsVisibility.isOn = false;
        }




        if (LampsVisibilityOption == "Yes")
        {
            for (int i = 0; i < Sec2IfDushNalBackStenkaDushLights.Count; i++)
            {
                Sec2IfDushNalBackStenkaDushLights[i].transform.localScale = new Vector3(1, 1, 1);
            }

            for (int i = 0; i < Sec2IfDushNalFromParilkaToDushLights.Count; i++)
            {
                Sec2IfDushNalFromParilkaToDushLights[i].transform.localScale = new Vector3(1, 1, 1);
            }

            for (int i = 0; i < Sec2IfDushNalFromDushToStenda2ShirinaLights.Count; i++)
            {
                Sec2IfDushNalFromDushToStenda2ShirinaLights[i].transform.localScale = new Vector3(1, 1, 1);
            }







            for (int i = 0; i < TopWallSection2Lamps.Count; i++)
            {
                TopWallSection2Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }


            for (int i = 0; i < BotWallSection2Lamps.Count; i++)
            {
                BotWallSection2Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }


            for (int i = 0; i < RightWallSection2Lamps.Count; i++)
            {
                RightWallSection2Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }

            for (int i = 0; i < LeftWallSection2Lamps.Count; i++)
            {
                LeftWallSection2Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }

            ///////////

            for (int i = 0; i < TopWallSection3Lamps.Count; i++)
            {
                TopWallSection3Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }


            for (int i = 0; i < BotWallSection3Lamps.Count; i++)
            {
                BotWallSection3Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }


            for (int i = 0; i < RightWallSection3Lamps.Count; i++)
            {
                RightWallSection3Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }

            for (int i = 0; i < LeftWallSection3Lamps.Count; i++)
            {
                LeftWallSection3Lamps[i].transform.localScale = new Vector3(1, 1, 1);
            }


            UglovLampLeftTopInParilka.transform.localScale = new Vector3(1, 1, 1);
            UglovLampRightTopInParilka.transform.localScale = new Vector3(1, 1, 1);
            UglovLampBottomInParilka.transform.localScale = new Vector3(1, 1, 1);


            ToggleLampsVisibility.isOn = true;
        }
        else if (LampsVisibilityOption == "No")
        {

            for (int i = 0; i < Sec2IfDushNalBackStenkaDushLights.Count; i++)
            {
                
                Sec2IfDushNalBackStenkaDushLights[i].transform.localScale = new Vector3(0, 0, 0);
                
            }

            for (int i = 0; i < Sec2IfDushNalFromParilkaToDushLights.Count; i++)
            {
                Sec2IfDushNalFromParilkaToDushLights[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < Sec2IfDushNalFromDushToStenda2ShirinaLights.Count; i++)
            {
                Sec2IfDushNalFromDushToStenda2ShirinaLights[i].transform.localScale = new Vector3(0, 0, 0);
            }



            for (int i = 0; i < TopWallSection2Lamps.Count; i++)
            {

                
                
                TopWallSection2Lamps[i].transform.localScale = new Vector3(0, 0, 0);
                
            }

            for (int i = 0; i < BotWallSection2Lamps.Count; i++)
            {
                BotWallSection2Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < RightWallSection2Lamps.Count; i++)
            {
                RightWallSection2Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < LeftWallSection2Lamps.Count; i++)
            {
                LeftWallSection2Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }
            /////////////////////////


            for (int i = 0; i < TopWallSection3Lamps.Count; i++)
            {
                TopWallSection3Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < BotWallSection3Lamps.Count; i++)
            {
                BotWallSection3Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < RightWallSection3Lamps.Count; i++)
            {
                RightWallSection3Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            for (int i = 0; i < LeftWallSection3Lamps.Count; i++)
            {
                LeftWallSection3Lamps[i].transform.localScale = new Vector3(0, 0, 0);
            }

            UglovLampLeftTopInParilka.transform.localScale = new Vector3(0, 0, 0);
            UglovLampRightTopInParilka.transform.localScale = new Vector3(0, 0, 0);
            UglovLampBottomInParilka.transform.localScale = new Vector3(0, 0, 0);


            ToggleLampsVisibility.isOn = false;
        }






        if (SidushkiVisibilityOption == "Yes")
        {


            Sidushki.transform.localScale = new Vector3(1, 1, 0.9f);
            ToggleSidushkiVisibility.isOn = true;

        }
        else if (SidushkiVisibilityOption == "No")
        {


            Sidushki.transform.localScale = new Vector3(0, 0, 0);
            ToggleSidushkiVisibility.isOn = false;
        }





        if (DveriVisibilityOption == "Yes")
        {


           // HoleConfigBtnScript[] HoleConfigBtnScriptList = GameObject.FindObjectsOfTypeAll(typeof(HoleConfigBtnScript)) as HoleConfigBtnScript[];

            HoleScript[] HolesList = GameObject.FindObjectsOfTypeAll(typeof(HoleScript)) as HoleScript[];

            for (int i = 0; i < HolesList.Length; i++)
            {
                if (HolesList[i].holeType==HoleScript.HoleType.door)
                    if (HolesList[i].door3d != null)
                        HolesList[i].door3d.transform.localScale = new Vector3(1, 1, 1);
            }

            

            ToggleDveriVisibility.isOn = true;
        }
        else if (DveriVisibilityOption == "No")
        {

            HoleScript[] HolesList = GameObject.FindObjectsOfTypeAll(typeof(HoleScript)) as HoleScript[];

            for (int i = 0; i < HolesList.Length; i++)
            {
                if (HolesList[i].holeType == HoleScript.HoleType.door)
                    if (HolesList[i].door3d!=null)
                        HolesList[i].door3d.transform.localScale = new Vector3(0, 0, 0);
            }
            ToggleDveriVisibility.isOn = false;
        }









    }




    
    public void SetOptionForWallsTexturesVisibility()
    { 

        if (ToggleWallsTexturesVisibility.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "WallsTexturesVisible>", "</WallsTexturesVisible>"), "<WallsTexturesVisible>" + "Yes" + "</WallsTexturesVisible>");

        }
        else
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<WallsTexturesVisible>", "</WallsTexturesVisible>"), "<WallsTexturesVisible>" + "No" + "</WallsTexturesVisible>");
        }

    }


    public void ManageOptionWallsTexturesVisibility(string WallsTexturesVisibilityOption)
    {
        GameObject FloorGO = GameObject.Find("Floor"); 

        if (WallsTexturesVisibilityOption == "Yes")
        {

            // Veshalka.transform.localScale = new Vector3(1, 1, 1);

            WallsTexturesVisible = true;
            ToggleWallsTexturesVisibility.isOn = true;

          

        }
        else if (WallsTexturesVisibilityOption == "No")
        {
            //Veshalka.transform.localScale = new Vector3(0, 0, 0);
            WallsTexturesVisible = false;
            ToggleWallsTexturesVisibility.isOn = false;
 
        }


    }



    public void SetOptionFor220vPosition()
    {
        if (Toggle220Pos1.isOn)
        {


            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "1" + "</220Pos>");




        }

        if (Toggle220Pos2.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "2" + "</220Pos>");
        }

        if (Toggle220Pos3.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "3" + "</220Pos>");
        }

        if (Toggle220Pos4.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "4" + "</220Pos>");
        }

        if (Toggle220Pos5.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "5" + "</220Pos>");
        }

        if (Toggle220Pos6.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "6" + "</220Pos>");
        }
        if (Toggle220Pos7.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "7" + "</220Pos>");
        }
        if (Toggle220Pos8.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<220Pos>", "</220Pos>"), "<220Pos>" + "8" + "</220Pos>");
        }

    }
    public void ManageOptionFor220Position(string option)
    {

        if (option == "1")
        {
            _220vPos1.SetActive(true);
            // _220vPos1.transform.position = new Vector3(0, StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.y- _220_OtstupSverhu/100, 0 + _220_OtstupSKrayu/100);    
            _220vPos1.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - (float)_220_OtstupSKrayu / 100, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = true;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;

        }
        if (option == "2")
        {


            _220vPos1.SetActive(false);
            _220vPos2.SetActive(true);
            _220vPos2.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina2.GetComponent<WallScript>().WallCube1.transform.position.z - (float)_220_OtstupSKrayu / 100);

            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = true;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;

        }
        if (option == "3")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(true);
            _220vPos3.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina1.GetComponent<WallScript>().WallCube1.transform.position.z + (float)_220_OtstupSKrayu / 100);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = true;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;


        }
        if (option == "4")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(true);
            _220vPos4.transform.position = new Vector3(StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x - (float)_220_OtstupSKrayu / 100, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = true;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;

        }

        if (option == "5")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(true);
            _220vPos5.transform.position = new Vector3(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x + (float)_220_OtstupSKrayu / 100, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina1.GetComponent<WallScript>().WallCube4.transform.position.z);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = true;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;

        }

        if (option == "6")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(true);
            _220vPos6.transform.position = new Vector3(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina1.GetComponent<WallScript>().WallCube1.transform.position.z + (float)_220_OtstupSKrayu / 100);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(false);

            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = true;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = false;
        }

        if (option == "7")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(true);
            _220vPos7.transform.position = new Vector3(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina2.GetComponent<WallScript>().WallCube1.transform.position.z - (float)_220_OtstupSKrayu / 100);
            _220vPos8.SetActive(false);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = true;
            Toggle220Pos8.isOn = false;

        }


        if (option == "8")
        {
            _220vPos1.SetActive(false);
            _220vPos2.SetActive(false);
            _220vPos3.SetActive(false);
            _220vPos4.SetActive(false);
            _220vPos5.SetActive(false);
            _220vPos6.SetActive(false);
            _220vPos7.SetActive(false);
            _220vPos8.SetActive(true);
            _220vPos8.transform.position = new Vector3(StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x + (float)_220_OtstupSKrayu / 100, StenaShirina1_Parilka.GetComponent<WallScript>().WallCube3.transform.position.y - (float)_220_OtstupSverhu / 100, StenaDlina2.GetComponent<WallScript>().WallCube4.transform.position.z);
            Toggle220Pos1.isOn = false;
            Toggle220Pos2.isOn = false;
            Toggle220Pos3.isOn = false;
            Toggle220Pos4.isOn = false;
            Toggle220Pos5.isOn = false;
            Toggle220Pos6.isOn = false;
            Toggle220Pos7.isOn = false;
            Toggle220Pos8.isOn = true;
        }



    }

    public void SetOptionForHolesInWall1(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos,string[] HoleType, string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">"; 

        }

        

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall1Holes>", "</Wall1Holes>"), "<Wall1Holes>" + holesDescription + "</Wall1Holes>");
    }






    public void SetOptionForHolesInWall2(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }

        
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall2Holes>", "</Wall2Holes>"), "<Wall2Holes>" + holesDescription + "</Wall2Holes>");
    }
    public void SetOptionForHolesInWall3(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }
        
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall3Holes>", "</Wall3Holes>"), "<Wall3Holes>" + holesDescription + "</Wall3Holes>");
    }

    public void SetOptionForHolesInWall4(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }

        
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall4Holes>", "</Wall4Holes>"), "<Wall4Holes>" + holesDescription + "</Wall4Holes>");
    }
    public void SetOptionForHolesInWall5(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }

         
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall5Holes>", "</Wall5Holes>"), "<Wall5Holes>" + holesDescription + "</Wall5Holes>");
    }
    public void SetOptionForHolesInWall6(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }

         
        
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall6Holes>", "</Wall6Holes>"), "<Wall6Holes>" + holesDescription + "</Wall6Holes>");
    }
    public void SetOptionForHolesInWall7(int HolesCount, Vector3[] Cube1Pos, Vector3[] Cube3Pos, string[] HoleType,string[] isStandartized)
    {
        string holesDescription = "<HolesCount>" + HolesCount + "</HolesCount>";
        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<Hole_" + (i + 1) + "_Cube1Pos>" + "<Cube1x>" + Cube1Pos[i].x + "</Cube1x>" + "<Cube1y>" + Cube1Pos[i].y + "</Cube1y>" + "<Cube1z>" + Cube1Pos[i].z + "</Cube1z>" + "</Hole_" + (i + 1) + "_Cube1Pos>" + "<Hole_" + (i + 1) + "_Cube3Pos>" + "<Cube3x>" + Cube3Pos[i].x + "</Cube3x>" + "<Cube3y>" + Cube3Pos[i].y + "</Cube3y>" + "<Cube3z>" + Cube3Pos[i].z + "</Cube3z>" + "</Hole_" + (i + 1) + "_Cube3Pos>";
        }

        for (int i = 0; i < HolesCount; i++)
        {
            holesDescription = holesDescription + "<HoleType" + (i + 1) + ">" + HoleType[i] + "</HoleType" + (i + 1) + ">";
            holesDescription = holesDescription + "<Standartized" + (i + 1) + ">" + isStandartized[i] + "</Standartized" + (i + 1) + ">";
        }

        
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<Wall7Holes>", "</Wall7Holes>"), "<Wall7Holes>" + holesDescription + "</Wall7Holes>");
    }

    public void ManageOptionForHolesInWall7(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos, string[] HoleTypeAsString, string[] Standartized)
    {
        /*

        if (CodeReadedFromFile)
        {
            
            #region HolesInit   
            foreach (HoleScript hs in StenkaDusha.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            StenkaDusha.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
                Debug.Log(HoleTypeAsString[i]);
                Debug.Log("Standartized:" + Standartized[i]);
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = StenkaDusha.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = StenkaDusha;
                    StenkaDusha.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdushinaZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = StenkaDusha.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = StenkaDusha;
                    StenkaDusha.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = StenkaDusha.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = StenkaDusha;
                    StenkaDusha.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion
        }
        */






        for (int i = 0; i < HolesCount; i++)
        {
            StenkaDusha.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            StenkaDusha.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube3Pos[i].z);
            StenkaDusha.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube3Pos[i].z);
            StenkaDusha.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);

            if (StenkaDusha.GetComponent<WallScript>().Holes[i].door3d != null)
                if (StenkaDusha.GetComponent<WallScript>().Holes[i].door3d.Find("DveriWood") != null)
                    StenkaDusha.GetComponent<WallScript>().Holes[i].door3d.Find("DveriWood").gameObject.SetActive(false);

            //if (DoorOrProemInSec2 == "Door")
            //{

            //    if (HoleTypeAsString[i] == "DoorIndoorRight")
            //    {

            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 120, 0);
            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;


            //    }

            //    if (HoleTypeAsString[i] == "DoorOutdoorRight")
            //    {


            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 60, 0);
            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;

            //    }

            //    if (HoleTypeAsString[i] == "DoorIndoorLeft")
            //    {
            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 240, 0);
            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
            //    }


            //    if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            //    {

            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 300, 0);
            //        Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;

            //    }
            //}

            if (Standartized[i] == "Standartized")
            {
                StenkaDusha.GetComponent<WallScript>().Holes[i].isStandartized = true;
            }
            else
              if (Standartized[i] == "NonStandartized")
            {
                StenkaDusha.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }


        }



    }

    public void ManageOptionForHolesInWall1(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos, string[] HoleTypeAsString, string[] Standartized)
    {
        if (CodeReadedFromFile)
        {
            GameObject currentWall = StenaDlina1;
            #region HolesInit   
            foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            currentWall.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
              //  Debug.Log(HoleTypeAsString[i]);
               // Debug.Log("Standartized:" + Standartized[i]);
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdushinaZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion

        }



        for (int i = 0; i < HolesCount; i++)
        {

            //Debug.Log(Standartized[i]);
            //Debug.Log(HoleTypeAsString[i]);
            

            StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = Holes_Cube1Pos[i];
            StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube3Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = Holes_Cube3Pos[i];
            StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);


            if (StenaDlina1.GetComponent<WallScript>().Holes[i].door3d!= null)
            StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.Find("DveriGlass").gameObject.SetActive(false);


            if (HoleTypeAsString[i]== "DoorIndoorRight")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 210, 0);
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.position = Holes_Cube1Pos[i];
                StenaDlina1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorRight";
            }

            if (HoleTypeAsString[i] == "DoorOutdoorRight")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 150, 0);
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.position = Holes_Cube1Pos[i];
                StenaDlina1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorRight";
            }

            if (HoleTypeAsString[i] == "DoorIndoorLeft")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 330, 0);
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.position = StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaDlina1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorLeft";
            }


            if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 30, 0);
                StenaDlina1.GetComponent<WallScript>().Holes[i].door3d.position = StenaDlina1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaDlina1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorLeft";
            }

            if (Standartized[i]=="Standartized")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].isStandartized = true;
                // StenaDlina1.GetComponent<WallScript>().Holes[i].HoleStandartisation();
                // StenaDlina1.GetComponent<WallScript>().Holes[i].HoleStandartisation();
            }
            else
                if (Standartized[i] == "NonStandartized")
            {
                StenaDlina1.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }



        }
    }
    public void ManageOptionForHolesInWall2(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos, string[] HoleTypeAsString, string[] Standartized)
    {
        if (CodeReadedFromFile)
        {
            GameObject currentWall = StenaDlina2;
            #region HolesInit   
            foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            currentWall.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
                Debug.Log(HoleTypeAsString[i]);
               
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdushinaZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorZGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion

        }





        for (int i = 0; i < HolesCount; i++)
        {
            StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = Holes_Cube1Pos[i];
            StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube3Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = Holes_Cube3Pos[i];
            StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);


            if (StenaDlina2.GetComponent<WallScript>().Holes[i].door3d != null)
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.Find("DveriGlass").gameObject.SetActive(false);

            if (HoleTypeAsString[i] == "DoorIndoorRight")
            {
                
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 30, 0);
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.position = StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaDlina2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorRight";
            }

            if (HoleTypeAsString[i] == "DoorOutdoorRight")
            {
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, -30, 0);
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.position = StenaDlina2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaDlina2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorRight";
            }

            if (HoleTypeAsString[i] == "DoorIndoorLeft")
            {
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 150, 0);
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.position = Holes_Cube1Pos[i];
                StenaDlina2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorLeft";
            }


            if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            {
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 210, 0);
                StenaDlina2.GetComponent<WallScript>().Holes[i].door3d.position = Holes_Cube1Pos[i];
                StenaDlina2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorLeft";
            }


            if (Standartized[i] == "Standartized")
            {
                StenaDlina2.GetComponent<WallScript>().Holes[i].isStandartized = true;
             //   StenaDlina2.GetComponent<WallScript>().Holes[i].HoleStandartisation();
            }
            else
               if (Standartized[i] == "NonStandartized")
            {
                StenaDlina2.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }

        }
    }
    public void ManageOptionForHolesInWall3(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos,string[] HoleTypeAsString, string[] Standartized)
    {
        if (CodeReadedFromFile)
        {
            GameObject currentWall = StenaShirina1_Parilka;
            #region HolesInit   
            foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            currentWall.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
                Debug.Log(HoleTypeAsString[i]);
                Debug.Log("Standartized:" + Standartized[i]);
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdusinaXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion
        }


        for (int i = 0; i < HolesCount; i++)
        {
            if (StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].door3d != null)
                StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].door3d.Find("DveriGlass").gameObject.SetActive(false);
            //тут была ошибка, окно выходило за рамки стен
            //StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            //StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube3Pos[i].z);
            //StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube3Pos[i].z);
            //StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);
            











            if (Standartized[i] == "Standartized")
            {
                StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].isStandartized = true;
            }
            else
              if (Standartized[i] == "NonStandartized")
            {
                StenaShirina1_Parilka.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }


        }
    }
    public void ManageOptionForHolesInWall4(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos,string[] HoleTypeAsString, string[] Standartized)
    {

        if (CodeReadedFromFile)
        {
            GameObject currentWall = StenaShirina2_SDveryu;
            #region HolesInit   
            foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            currentWall.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
                Debug.Log(currentWall.name +" "+HoleTypeAsString[i]);
            
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdusinaXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion
        }

        for (int i = 0; i < HolesCount; i++)
        {
            if (StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d != null)
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.Find("DveriGlass").gameObject.SetActive(false);

            StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube3Pos[i].z);
            StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube3Pos[i].z);
            StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);

            if (HoleTypeAsString[i] == "DoorIndoorRight")
            {

                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 120, 0);
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.position = StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorRight";

            }

            if (HoleTypeAsString[i] == "DoorOutdoorRight")
            {


                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 60, 0);
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.position = StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorRight";
            }

            if (HoleTypeAsString[i] == "DoorIndoorLeft")
            {
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 240, 0);
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.position = StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorLeft";


            }


            if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            {
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 300, 0);
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].door3d.position = StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorLeft";
            }

            if (Standartized[i] == "Standartized")
            {
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].isStandartized = true;
            }
            else
              if (Standartized[i] == "NonStandartized")
            {
                StenaShirina2_SDveryu.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }

        }
    }
    public void ManageOptionForHolesInWall5(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos, string[] HoleTypeAsString, string[] Standartized)
    {
        /*
        if (CodeReadedFromFile)
        {
            GameObject currentWall = Peregorodka1;
            #region HolesInit   
            foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
            {
                Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
                Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
            }

            currentWall.GetComponent<WallScript>().Holes.Clear();


            for (int i = 0; i < HolesCount; i++)
            {
                Debug.Log(HoleTypeAsString[i]);
                Debug.Log("Standartized:" + Standartized[i]);
                if (HoleTypeAsString[i] == "Window")
                {
                    GameObject tempGO = GameObject.Instantiate(WindowXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


                }
                else
                    if (HoleTypeAsString[i] == "Otdushina")
                {
                    GameObject tempGO = GameObject.Instantiate(OtdusinaXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                        if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
                }

                else
                    if (HoleTypeAsString[i].Contains("Door"))
                {
                    GameObject tempGO = GameObject.Instantiate(DoorXGOToInstanciate);
                    tempGO.SetActive(true);
                    tempGO.transform.parent = currentWall.transform;
                    tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                    currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                    if (Standartized[i] == "NonStandartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                    else
                    if (Standartized[i] == "Standartized")
                        tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

                }

            }

            #endregion
        }
        */



        for (int i = 0; i < HolesCount; i++)
        {
            if (Peregorodka1.GetComponent<WallScript>().Holes[i].door3d != null)
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.Find("DveriWood").gameObject.SetActive(false); // .gameObject.SetActive(false);

            Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube3Pos[i].z);
            Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube3Pos[i].z);
            Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);

            

            if (HoleTypeAsString[i] == "DoorIndoorRight")
            {

                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 120, 0);
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                Peregorodka1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorRight";

            }
             

            if (HoleTypeAsString[i] == "DoorOutdoorRight")
            {

                
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 60, 0);
                
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                Peregorodka1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorRight";
            }

            if (HoleTypeAsString[i] == "DoorIndoorLeft")
            {
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 240, 0);
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;

                Peregorodka1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorLeft";

            }


            if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            {
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 300, 0);
                Peregorodka1.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka1.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                Peregorodka1.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorLeft";
            }

            if (Standartized[i] == "Standartized")
            {
                Peregorodka1.GetComponent<WallScript>().Holes[i].isStandartized = true;
            }
            else
              if (Standartized[i] == "NonStandartized")
            {
                Peregorodka1.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }

        }
    }
    public void ManageOptionForHolesInWall6(int HolesCount, Vector3[] Holes_Cube1Pos, Vector3[] Holes_Cube3Pos, string[] HoleTypeAsString, string[] Standartized)
    {
        /*
        if (CodeReadedFromFile)
        {
            GameObject currentWall = Peregorodka2;
        #region HolesInit   
        foreach (HoleScript hs in currentWall.GetComponentsInChildren<HoleScript>())
        {
            Destroy(hs.HoleConfigBtnOfThisHole, 0.001f);
            Destroy(hs.gameObject.transform.parent.gameObject, 0.001f);
        }

        currentWall.GetComponent<WallScript>().Holes.Clear();


        for (int i = 0; i < HolesCount; i++)
        {
            Debug.Log(HoleTypeAsString[i]);
            Debug.Log("Standartized:" + Standartized[i]);
            if (HoleTypeAsString[i] == "Window")
            {
                GameObject tempGO = GameObject.Instantiate(WindowXGOToInstanciate);
                tempGO.SetActive(true);
                tempGO.transform.parent = currentWall.transform;
                tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                if (Standartized[i] == "NonStandartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                else
                    if (Standartized[i] == "Standartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;


            }
            else
                if (HoleTypeAsString[i] == "Otdushina")
            {
                GameObject tempGO = GameObject.Instantiate(OtdusinaXGOToInstanciate);
                tempGO.SetActive(true);
                tempGO.transform.parent = currentWall.transform;
                tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                if (Standartized[i] == "NonStandartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                else
                    if (Standartized[i] == "Standartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;
            }

            else
                if (HoleTypeAsString[i].Contains("Door"))
            {
                GameObject tempGO = GameObject.Instantiate(DoorXGOToInstanciate);
                tempGO.SetActive(true);
                tempGO.transform.parent = currentWall.transform;
                tempGO.GetComponentInChildren<HoleScript>().ParentWall = currentWall;
                currentWall.GetComponent<WallScript>().Holes.Add(tempGO.GetComponentInChildren<HoleScript>());


                if (Standartized[i] == "NonStandartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = false;
                else
                if (Standartized[i] == "Standartized")
                    tempGO.GetComponentInChildren<HoleScript>().isStandartized = true;

            }

        }

        #endregion
        }



        */









        for (int i = 0; i < HolesCount; i++)
        {
            Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
            Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube3Pos[i].z);
            Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube3Pos[i].z);
            Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);

            if (Peregorodka2.GetComponent<WallScript>().Holes[i].door3d != null)
                if (Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.Find("DveriWood")!=null)
                    Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.Find("DveriWood").gameObject.SetActive(false);

            if (DoorOrProemInSec2 == "Door")
            { 

            if (HoleTypeAsString[i] == "DoorIndoorRight")
            {

                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 120, 0);
                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                    Peregorodka2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorRight";

                }

            if (HoleTypeAsString[i] == "DoorOutdoorRight")
            {


                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 60, 0);
                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position;
                    Peregorodka2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorRight";
                }

            if (HoleTypeAsString[i] == "DoorIndoorLeft")
            {
                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 240, 0);
                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                    Peregorodka2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "IndoorLeft";
                }


            if (HoleTypeAsString[i] == "DoorOutdoorLeft")
            {

                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.eulerAngles = new Vector3(0, 300, 0);
                Peregorodka2.GetComponent<WallScript>().Holes[i].door3d.position = Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position;
                    Peregorodka2.GetComponent<WallScript>().Holes[i].DoorOpeningDirectionInfo = "OutdoorLeft";
                }



          


            }

            /* Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube1.transform.position = Holes_Cube1Pos[i];
             Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube2.transform.position = new Vector3(Holes_Cube3Pos[i].x, Holes_Cube1Pos[i].y, Holes_Cube1Pos[i].z);
             Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube3.transform.position = Holes_Cube3Pos[i];
             Peregorodka2.GetComponent<WallScript>().Holes[i].HoleCube4.transform.position = new Vector3(Holes_Cube1Pos[i].x, Holes_Cube3Pos[i].y, Holes_Cube1Pos[i].z);
             */

            if (Standartized[i] == "Standartized")
            {
                Peregorodka2.GetComponent<WallScript>().Holes[i].isStandartized = true;
            }
            else
                  if (Standartized[i] == "NonStandartized")
            {
                Peregorodka2.GetComponent<WallScript>().Holes[i].isStandartized = false;
            }

        }

                

    }

    public void HolesStandartize()
    {

        foreach(HoleScript hs in StenaDlina1.GetComponent<WallScript>().Holes)
        {
            hs.HoleStandartisation();
        }
        foreach (HoleScript hs in StenaDlina2.GetComponent<WallScript>().Holes)
        {

            hs.HoleStandartisation();
        }
        foreach (HoleScript hs in StenaShirina1_Parilka.GetComponent<WallScript>().Holes)
        {
           
              hs.HoleStandartisation();
        }
        foreach (HoleScript hs in StenaShirina2_SDveryu.GetComponent<WallScript>().Holes)
        {
            hs.HoleStandartisation();
        }
        //foreach (HoleScript hs in Peregorodka1.GetComponent<WallScript>().Holes)
        //{
        //    hs.HoleStandartisation();
        //}
        //foreach (HoleScript hs in Peregorodka2.GetComponent<WallScript>().Holes)
        //{
        //    hs.HoleStandartisation();
        //}

      
    }



    public void SetOptionForSidePolkaVParilke()
    {



        if (DropdownOptionForSidePolkaVParilke.value == 0)  //не нужна
        {
            //SidePolkaParilkaLeft.SetActive(false);
            //SidePolkaParilkaRight.SetActive(false);
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<OptionForSidePolkaVParilke>", "</OptionForSidePolkaVParilke>"), "<OptionForSidePolkaVParilke>" + "notNeeded" + "</OptionForSidePolkaVParilke>");
        }

        if (DropdownOptionForSidePolkaVParilke.value == 1)  //справа
        {
            //SidePolkaParilkaLeft.SetActive(false);
            //SidePolkaParilkaRight.SetActive(true);


            DropdownPechRightOrLeft.value = 0;
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<OptionForSidePolkaVParilke>", "</OptionForSidePolkaVParilke>"), "<OptionForSidePolkaVParilke>" + "right" + "</OptionForSidePolkaVParilke>");



        }
        if (DropdownOptionForSidePolkaVParilke.value == 2)  //слева
        {
            //SidePolkaParilkaLeft.SetActive(true);
            //SidePolkaParilkaRight.SetActive(false);

            //CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechRightOrLeft>", "</PechRightOrLeft>"), "<PechRightOrLeft>" + "Right" + "</PechRightOrLeft>");

            DropdownPechRightOrLeft.value = 1;
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<OptionForSidePolkaVParilke>", "</OptionForSidePolkaVParilke>"), "<OptionForSidePolkaVParilke>" + "left" + "</OptionForSidePolkaVParilke>");


        }





    }
    public void ManageOptionForSidePolkaVParilke(string option)
    {
        if (option == "notNeeded")
        {
            SidePolkaParilkaLeft.SetActive(false);
            SidePolkaParilkaRight.SetActive(false);
            DropdownOptionForSidePolkaVParilke.value = 0;
        }
        if (option == "right")
        {
            SidePolkaParilkaLeft.SetActive(false);
            SidePolkaParilkaRight.SetActive(true);
            DropdownOptionForSidePolkaVParilke.value = 1;
        }

        if (option == "left")
        {
            SidePolkaParilkaLeft.SetActive(true);
            SidePolkaParilkaRight.SetActive(false);
            DropdownOptionForSidePolkaVParilke.value = 2;
        }

    }
    public void SetOptionForDushPos(int ToggleOfPosition)
    {

        if (ToggleOfPosition == 1)
        {
            
           // DushPos =
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushPos>", "</DushPos>"), "<DushPos>" + "DushPos1" + "</DushPos>");
        }

        if (ToggleOfPosition == 2)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushPos>", "</DushPos>"), "<DushPos>" + "DushPos2" + "</DushPos>");
        }

        if (ToggleOfPosition == 3)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushPos>", "</DushPos>"), "<DushPos>" + "DushPos3" + "</DushPos>");
        }

        if (ToggleOfPosition == 4)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushPos>", "</DushPos>"), "<DushPos>" + "DushPos4" + "</DushPos>");
        }
    }
 



    public void ManageOptionForDushPos(string option, string optionIndoorOrOutDoorPech)
    {

        float widthOfStenkaDusha = 0.9f;


        //тут
        if (optionIndoorOrOutDoorPech=="Indoor")
        {

            if (option == "DushPos4")
            {

                option = "DushPos1";
            }


            if (option == "DushPos3")
            {

                option = "DushPos2";
            }

        }


        if (option == "DushPos1")
        {
            Dush.transform.position = DushTransfrom1.position;      ///прпедварительная (по оси Z)
            Dush.transform.rotation = DushTransfrom1.rotation;
            ToggleDushPos1.isOn = true;
            ToggleDushPos2.isOn = false;
            ToggleDushPos3.isOn = false;
            ToggleDushPos4.isOn = false;
            DushPos = "DushPos1";


            //  передвигаем стенку по Z душа HOLESCRIPT

            //float widthOfStenkaDusha = 0.9f; //Mathf.Abs(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleLine2.transform.position.z - StenkaDusha.GetComponent<WallScript>().Holes[0].HoleLine4.transform.position.z);

            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position.y, widthOfStenkaDusha+ TolschinaStenKarkasa);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.y, widthOfStenkaDusha+ TolschinaStenKarkasa);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position.y, BanyaWidth/100f- TolschinaStenKarkasa);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.y, BanyaWidth / 100f - TolschinaStenKarkasa);
            
            
            //
            StenkaDushaRightOrLeft = "Left";

        }

        if (option == "DushPos2")
        {
            Dush.transform.position = DushTransfrom2.position;    ///прпедварительная (по оси Z)
            Dush.transform.rotation = DushTransfrom2.rotation;
            DushPos = "DushPos2";
            ToggleDushPos1.isOn = false;
            ToggleDushPos2.isOn = true;
            ToggleDushPos3.isOn = false;
            ToggleDushPos4.isOn = false;

            //  передвигаем стенку душа HOLESCRIPT
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube2.transform.position.y, TolschinaStenKarkasa);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube3.transform.position.y, TolschinaStenKarkasa);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube1.transform.position.y, BanyaWidth / 100f - TolschinaStenKarkasa  - widthOfStenkaDusha);
            StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position = new Vector3(StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.x, StenkaDusha.GetComponent<WallScript>().Holes[0].HoleCube4.transform.position.y, BanyaWidth / 100f - TolschinaStenKarkasa - widthOfStenkaDusha);

            //

            StenkaDushaRightOrLeft = "Right";

        }
        if (option == "DushPos3")
        {
            Dush.transform.position = DushTransfrom3.position;      ///прпедварительная (по оси Z)
            Dush.transform.rotation = DushTransfrom3.rotation;
            DushPos = "DushPos3";
            ToggleDushPos1.isOn = false;
            ToggleDushPos2.isOn = false;
            ToggleDushPos3.isOn = true;
            ToggleDushPos4.isOn = false;
        }
        if (option == "DushPos4")
        {
            Dush.transform.position = DushTransfrom4.position;      ///прпедварительная (по оси Z)
            Dush.transform.rotation = DushTransfrom4.rotation;
            DushPos = "DushPos4";
            ToggleDushPos1.isOn = false;
            ToggleDushPos2.isOn = false;
            ToggleDushPos3.isOn = false;
            ToggleDushPos4.isOn = true;
        }


        if ((PechIndoorOrOutdoor == "Indoor") && (SectionCount == 2))
        {
             
            if (option == "DushPos1")
                Dush.transform.position = new Vector3(XposOfStenkaDusha/100f - 0.552f+  0.5f*TolschinaStenKarkasa, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos2")
                Dush.transform.position = new Vector3(XposOfStenkaDusha/100f - 0.95f + 0.5f * TolschinaStenKarkasa, Dush.transform.position.y, Dush.transform.position.z);
        }

        if ((PechIndoorOrOutdoor == "Outdoor") && (SectionCount == 2))
        {

            if (option == "DushPos1")
                Dush.transform.position = new Vector3(XposOfStenkaDusha / 100f - 0.552f + 0.5f * TolschinaStenKarkasa, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos2")
                Dush.transform.position = new Vector3(XposOfStenkaDusha / 100f - 0.95f + 0.5f * TolschinaStenKarkasa, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos3")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f + 0.651f, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos4")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f - 0.552f + 1.6092f, Dush.transform.position.y, Dush.transform.position.z);

        }


        if ((PechIndoorOrOutdoor == "Indoor") && (SectionCount == 3))
        {
            if (option == "DushPos1")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f - 0.552f + 0.3f, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos2")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f - 0.95f + 0.3f, Dush.transform.position.y, Dush.transform.position.z);
        }

        if ((PechIndoorOrOutdoor == "Outdoor") && (SectionCount == 3))
        {
            if (option == "DushPos1")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f - 0.552f + 0.3f, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos2")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaMoechnoi / 100f - 0.95f + 0.3f, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos3")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f + 0.651f, Dush.transform.position.y, Dush.transform.position.z);
            if (option == "DushPos4")
                Dush.transform.position = new Vector3(XPosOfPeregorodkaParilnogo / 100f - 0.552f + 1.6092f, Dush.transform.position.y, Dush.transform.position.z);
        }







    }
    public void SetBanyaWidthFromInputField()
    {

        BanyaWidth = float.Parse(BanayWidthIF.text);
        BanyaWidthInfo.text = float.Parse(BanayWidthIF.text).ToString();   //               BanyaWidth.ToString();
        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaWidth>", "</BanyaWidth>"), "<BanyaWidth>" + float.Parse(BanayWidthIF.text) + "</BanyaWidth>");
    }
    public void ManageOptionBanyaWidthFromDropdownList(int width)
    {
        BanyaWidth = width;
        BanayWidthIF.text = width.ToString();
        // BanyaGeneralInfoGO.GetComponent<BanyaGeneralInfoScript>().
    }
    public void SetBanyaLengthFromDropdownList()
    {
        /*
        string bigStolNalichieBeforeChangingLegnthFromDropdown;
             bigStolNalichieBeforeChangingLegnthFromDropdown = BigStolNalichie;
        string malStolNalichieBeforeChangingLegnthFromDropdown;
        malStolNalichieBeforeChangingLegnthFromDropdown = MalStolNalichie;*/

        LastBanyaLengthSetFrom = BanyaLengthSetFromType.FromDropdownList;

        //    MalStolNalichie = "NoMalStol";
        //    ToggleNalichieMalStola.isOn = false;
        //}
        //    if (option == "MalStolEnabled")
        //    {
        //        MalStolGO.SetActive(true);
        //        MalStolNalichie = "MalStolEnabled"

        /*
        if ((StandartDropdown.value == 0)||(StandartDropdown.value == 1)|| (StandartDropdown.value == 2))

      
        {
            //BigStolNalichie = "NoBigStol";

            //ToggleNalichieBigStola.isOn = false;
            //SetOptionForBigStolNalichie();
            Debug.Log(BigStolNalichie);
            if (BigStolNalichie == "BigStolEnabled")
            BigStolGO.SetActive(false);
            ToggleNalichieMalStola.gameObject.SetActive(true);
            ToggleNalichieBigStola.gameObject.SetActive(false);
        }
        else
        {
            if (MalStolNalichie == "MalStolEnabled")
                MalStolGO.SetActive(false);
            //MalStolNalichie = "NoMalStol";

            ToggleNalichieMalStola.gameObject.SetActive(false);
            ToggleNalichieBigStola.gameObject.SetActive(true);
            //SetOptionForMalStolNalichie();
        }

        */



        if (StandartDropdown.value == 0)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 400 + "</BanyaLength>");  //BanyaLength = 400.0f;
            BanyaLengthInfo.text = "400";
            BanayLengthIF.text = "400";

        }
        if (StandartDropdown.value == 1)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 450 + "</BanyaLength>");
            BanyaLengthInfo.text = "450";
            BanayLengthIF.text = "450";

        }
        if (StandartDropdown.value == 2)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 500 + "</BanyaLength>");
            BanyaLengthInfo.text = "500";
            BanayLengthIF.text = "500";
        }
        if (StandartDropdown.value == 3)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 550 + "</BanyaLength>");  //BanyaLength = 400.0f;
            BanyaLengthInfo.text = "550";
            BanayLengthIF.text = "550";

        }
        if (StandartDropdown.value == 4)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 600 + "</BanyaLength>");
            BanyaLengthInfo.text = "600";
            BanayLengthIF.text = "600";
        }

        if (StandartDropdown.value == 5)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 650 + "</BanyaLength>");
            BanyaLengthInfo.text = "650";
            BanayLengthIF.text = "650";

        }

        if (StandartDropdown.value == 6)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 700 + "</BanyaLength>");
            BanyaLengthInfo.text = "700";
            BanayLengthIF.text = "700";
        }

        if (StandartDropdown.value == 7)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 750 + "</BanyaLength>");
            BanyaLengthInfo.text = "750";
            BanayLengthIF.text = "750";

        }

        if (StandartDropdown.value == 8)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + 800 + "</BanyaLength>");
            BanyaLengthInfo.text = "800";
            BanayLengthIF.text = "800";

        }
  



    }
    public void ManageOptionBanyaLengthFromDropdownList(int length)
    {
        BanyaLength = length;
        if (length == 400)
            StandartDropdown.value = 0;
        if (length == 450)
            StandartDropdown.value = 1;
        if (length == 500)
            StandartDropdown.value = 2;
        if (length == 550)
            StandartDropdown.value = 3;
        if (length == 600)
            StandartDropdown.value = 4;
        if (length == 650)
            StandartDropdown.value = 5;
        if (length == 700)
            StandartDropdown.value = 6;
        if (length == 750)
            StandartDropdown.value = 7;
        if (length == 800)
            StandartDropdown.value = 8;

        //SetOptionRasstOtParilkiToStenkaDusha();
        // BanyaGeneralInfoGO.GetComponent<BanyaGeneralInfoScript>().
    }

    public void WriteToCodeBanyaLengthFromInputField()
    {
        LastBanyaLengthSetFrom = BanyaLengthSetFromType.FromInputField;

        BanyaLengthInfo.text = float.Parse(BanayLengthIF.text).ToString();

        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BanyaLength>", "</BanyaLength>"), "<BanyaLength>" + float.Parse(BanayLengthIF.text) + "</BanyaLength>");

    }

    public void OpenSite(string linkAddress)
    {
        Application.OpenURL(linkAddress);
    }
    public void SetOptionSectionCount()
    {
        if ((UIDropdownForSectionCount.value == 0))//&& (DushNalichie== "NoDush"))
        {
            DropdownSelectionOfSectionForLightGeneration.value = 0;
            DropdownSelectionOfSectionForLightGeneration.interactable = false;
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SectionCount>", "</SectionCount>"), "<SectionCount>" + "2" + "</SectionCount>");
        }
        else
        {
            if (UIDropdownForSectionCount.value == 1)
            {
                DropdownSelectionOfSectionForLightGeneration.interactable = true;
                CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SectionCount>", "</SectionCount>"), "<SectionCount>" + "3" + "</SectionCount>");
            }
        }

        

}

    public void ManageOptionSectionCount(string option)
    {

        if (option == "2")
        {
            Peregorodka2.SetActive(false);
            SectionCount = 2;
            UIDropdownForSectionCount.value = 0;

            UIMoechnayaDlinaIF.gameObject.SetActive(false);
            UIMoechnayaDlinaText.gameObject.SetActive(false);
            //StenkaDusha.SetActive(true);
        }
        else
        {
            if (option == "3")
            {
                Peregorodka2.SetActive(true);
                SectionCount = 3;
                UIDropdownForSectionCount.value = 1;
                StenkaDusha.SetActive(false);
                UIMoechnayaDlinaIF.gameObject.SetActive(true);
                UIMoechnayaDlinaText.gameObject.SetActive(true);
            }
        }
    }
    public void ManageOptionForSlivPosition(string option)
    {
        if (option == "1")
        {
            Slive1MidPoShirineParilka.SetActive(true);
            Slive2MidParilkaSleva.SetActive(false);
            Slive3MidParilkaSprava.SetActive(false);
            Slive4MidMoechnayaSleva.SetActive(false);
            Slive5MidMoechnayaSprava.SetActive(false);
            ToggleSlivePos1.isOn = true;
            ToggleSlivePos5.isOn = false;
            ToggleSlivePos2.isOn = false;
            ToggleSlivePos3.isOn = false;
            ToggleSlivePos4.isOn = false;
        }
        if (option == "2")
        {
            Slive1MidPoShirineParilka.SetActive(false);
            Slive2MidParilkaSleva.SetActive(true);
            Slive2MidParilkaSleva.transform.position = new Vector3((StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, Slive2MidParilkaSleva.transform.position.y, Slive2MidParilkaSleva.transform.position.z);
            Slive3MidParilkaSprava.SetActive(false);
            Slive4MidMoechnayaSleva.SetActive(false);
            Slive5MidMoechnayaSprava.SetActive(false);
            ToggleSlivePos2.isOn = true;
            ToggleSlivePos1.isOn = false;
            ToggleSlivePos5.isOn = false;
            ToggleSlivePos3.isOn = false;
            ToggleSlivePos4.isOn = false;

        }
        if (option == "3")
        {
            Slive1MidPoShirineParilka.SetActive(false);
            Slive2MidParilkaSleva.SetActive(false);
            Slive3MidParilkaSprava.SetActive(true);
            Slive3MidParilkaSprava.transform.position = new Vector3((StenaShirina1_Parilka.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, Slive3MidParilkaSprava.transform.position.y, Slive3MidParilkaSprava.transform.position.z);
            Slive4MidMoechnayaSleva.SetActive(false);
            Slive5MidMoechnayaSprava.SetActive(false);
            ToggleSlivePos3.isOn = true;
            ToggleSlivePos1.isOn = false;
            ToggleSlivePos2.isOn = false;
            ToggleSlivePos5.isOn = false;
            ToggleSlivePos4.isOn = false;

        }
        if (option == "4")
        {
            Slive1MidPoShirineParilka.SetActive(false);
            Slive2MidParilkaSleva.SetActive(false);
            Slive3MidParilkaSprava.SetActive(false);
            Slive4MidMoechnayaSleva.SetActive(true);
            Slive4MidMoechnayaSleva.transform.position = new Vector3((Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, Slive4MidMoechnayaSleva.transform.position.y, Slive4MidMoechnayaSleva.transform.position.z);
            Slive5MidMoechnayaSprava.SetActive(false);
            ToggleSlivePos4.isOn = true;
            ToggleSlivePos1.isOn = false;
            ToggleSlivePos2.isOn = false;
            ToggleSlivePos3.isOn = false;
            ToggleSlivePos5.isOn = false;
        }

        if (option == "5")
        {
            Slive1MidPoShirineParilka.SetActive(false);
            Slive2MidParilkaSleva.SetActive(false);
            Slive3MidParilkaSprava.SetActive(false);
            Slive4MidMoechnayaSleva.SetActive(false);
            Slive5MidMoechnayaSprava.SetActive(true);
            ToggleSlivePos5.isOn = true;
            ToggleSlivePos1.isOn = false;
            ToggleSlivePos2.isOn = false;
            ToggleSlivePos3.isOn = false;
            ToggleSlivePos4.isOn = false;
            Slive5MidMoechnayaSprava.transform.position = new Vector3((Peregorodka2.GetComponent<WallScript>().WallCube1.transform.position.x + Peregorodka1.GetComponent<WallScript>().WallCube1.transform.position.x) / 2, Slive5MidMoechnayaSprava.transform.position.y, Slive5MidMoechnayaSprava.transform.position.z);
        }
    }

    public void SetOptionForSlivPosition()
    {
        if (ToggleSlivePos1.isOn)
        {
            /**/


            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SlivPos>", "</SlivPos>"), "<SlivPos>" + "1" + "</SlivPos>");




        }

        if (ToggleSlivePos2.isOn)
        {
            /* Slive1MidPoShirineParilka.SetActive(false);
             Slive2MidParilkaSleva.SetActive(true);
             Slive3MidParilkaSprava.SetActive(false);
             Slive4MidMoechnayaSleva.SetActive(false);
             Slive5MidMoechnayaSprava.SetActive(false);*/
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SlivPos>", "</SlivPos>"), "<SlivPos>" + "2" + "</SlivPos>");
        }

        if (ToggleSlivePos3.isOn)
        {
            /*  Slive1MidPoShirineParilka.SetActive(false);
              Slive2MidParilkaSleva.SetActive(false);
              Slive3MidParilkaSprava.SetActive(true);
              Slive4MidMoechnayaSleva.SetActive(false);
              Slive5MidMoechnayaSprava.SetActive(false);*/
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SlivPos>", "</SlivPos>"), "<SlivPos>" + "3" + "</SlivPos>");
        }

        if (ToggleSlivePos4.isOn)
        {
            /* Slive1MidPoShirineParilka.SetActive(false);
             Slive2MidParilkaSleva.SetActive(false);
             Slive3MidParilkaSprava.SetActive(false);
             Slive4MidMoechnayaSleva.SetActive(true);
             Slive5MidMoechnayaSprava.SetActive(false);*/
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SlivPos>", "</SlivPos>"), "<SlivPos>" + "4" + "</SlivPos>");
        }

        if (ToggleSlivePos5.isOn)
        {
            /*Slive1MidPoShirineParilka.SetActive(false);
            Slive2MidParilkaSleva.SetActive(false);
            Slive3MidParilkaSprava.SetActive(false);
            Slive4MidMoechnayaSleva.SetActive(false);
            Slive5MidMoechnayaSprava.SetActive(true);*/
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<SlivPos>", "</SlivPos>"), "<SlivPos>" + "5" + "</SlivPos>");
        }


    }
    public void SetOptionForNizhnyaPolkaInMiniParilkaScheme()
    {
        if (DropdownMiniParilkaNiznyaPolka.value == 0)
        {
            DropdownPechRightOrLeft.value = 1;
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<OptionForNizhnyaPolkaInMiniParilkaScheme>", "</OptionForNizhnyaPolkaInMiniParilkaScheme>"), "<OptionForNizhnyaPolkaInMiniParilkaScheme>" + "left" + "</OptionForNizhnyaPolkaInMiniParilkaScheme>");
        }

        if (DropdownMiniParilkaNiznyaPolka.value == 1)
        {
            DropdownPechRightOrLeft.value = 0;
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<OptionForNizhnyaPolkaInMiniParilkaScheme>", "</OptionForNizhnyaPolkaInMiniParilkaScheme>"), "<OptionForNizhnyaPolkaInMiniParilkaScheme>" + "right" + "</OptionForNizhnyaPolkaInMiniParilkaScheme>");
        }
    }
    public void ManageOptionForNizhnyaPolkaInMiniParilkaScheme(string option)
    {
        if (ParilkaSchemes.value == 0)
        {
            if (option == "left")
            {

                MiniParilkaNizhnyaLeftPolka.SetActive(true);
                MiniParilkaNizhnyaRightPolka.SetActive(false);
                DropdownMiniParilkaNiznyaPolka.value = 0;
            }


            if (option == "right")
            {
                MiniParilkaNizhnyaLeftPolka.SetActive(false);
                MiniParilkaNizhnyaRightPolka.SetActive(true);
                DropdownMiniParilkaNiznyaPolka.value = 1;
            }
        }
    }
    public void SetoptionForParilkaScheme()
    {

        if (ParilkaSchemes.value == 0)  //мини парилка
        {
            GO_Text_ifParilkaMini.SetActive(true);
            GO_Dropdown_ifParilkaMini.SetActive(true);

            GO_Text_ifParilkaMaxi.SetActive(false);
            GO_Dropdown_ifParilkaMaxi.SetActive(false);

            GO_Dropdown_ifParilkaStandart.SetActive(false);
            GO_Text_ifParilkaStandart.SetActive(false);


            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<ParilkaScheme>", "</ParilkaScheme>"), "<ParilkaScheme>" + "Mini" + "</ParilkaScheme>");



            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<XPositionOfPeregodorkaParilnogo>", "</XPositionOfPeregodorkaParilnogo>"), "<XPositionOfPeregodorkaParilnogo>" + 170 + "</XPositionOfPeregodorkaParilnogo>");

        }

        if (ParilkaSchemes.value == 1)   //стандарт
        {
            GO_Text_ifParilkaMini.SetActive(false);
            GO_Dropdown_ifParilkaMini.SetActive(false);

            GO_Text_ifParilkaMaxi.SetActive(false);
            GO_Dropdown_ifParilkaMaxi.SetActive(false);

            GO_Dropdown_ifParilkaStandart.SetActive(true);
            GO_Text_ifParilkaStandart.SetActive(true);

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<ParilkaScheme>", "</ParilkaScheme>"), "<ParilkaScheme>" + "Standart" + "</ParilkaScheme>");
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<XPositionOfPeregodorkaParilnogo>", "</XPositionOfPeregodorkaParilnogo>"), "<XPositionOfPeregodorkaParilnogo>" + 210 + "</XPositionOfPeregodorkaParilnogo>");
        }

        if (ParilkaSchemes.value == 2)  //макси
        {
            GO_Text_ifParilkaMini.SetActive(false);
            GO_Dropdown_ifParilkaMini.SetActive(false);

            GO_Text_ifParilkaMaxi.SetActive(true);
            GO_Dropdown_ifParilkaMaxi.SetActive(true);

            GO_Dropdown_ifParilkaStandart.SetActive(false);
            GO_Text_ifParilkaStandart.SetActive(false);

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<ParilkaScheme>", "</ParilkaScheme>"), "<ParilkaScheme>" + "Maxi" + "</ParilkaScheme>");
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<XPositionOfPeregodorkaParilnogo>", "</XPositionOfPeregodorkaParilnogo>"), "<XPositionOfPeregodorkaParilnogo>" + 250 + "</XPositionOfPeregodorkaParilnogo>");
        }

    }
    public void ManageOptionForParilkaScheme(string option)
    {
        if (option == "Mini")
        {
            SidePolkaParilkaLeft.SetActive(false);
            SidePolkaParilkaRight.SetActive(false);
            StandartNizhnyaPolka.SetActive(false);

            MaxiBokovyeSidushkiParent.SetActive(false);
            ParilkaSchemes.value = 0;
        }


        if (option == "Standart")
        {
            StandartNizhnyaPolka.SetActive(true);
            SidePolkaParilkaLeft.SetActive(false);
            SidePolkaParilkaRight.SetActive(false);
            MiniParilkaNizhnyaLeftPolka.SetActive(false);
            MiniParilkaNizhnyaRightPolka.SetActive(false);
            MaxiBokovyeSidushkiParent.SetActive(false);
            ParilkaSchemes.value = 1;
        }

        if (option == "Maxi")
        {
            SidePolkaParilkaLeft.SetActive(true);
            SidePolkaParilkaRight.SetActive(false);


            StandartNizhnyaPolka.SetActive(true);

            MiniParilkaNizhnyaLeftPolka.SetActive(false);
            MiniParilkaNizhnyaRightPolka.SetActive(false);
            MaxiBokovyeSidushkiParent.SetActive(true);
            ParilkaSchemes.value = 2;
        }

    }
    public void SetOptionPechRightOrLeft()
    {
        if (DropdownPechRightOrLeft.value == 0)
        {
          
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechRightOrLeft>", "</PechRightOrLeft>"), "<PechRightOrLeft>" + "Left" + "</PechRightOrLeft>");

        }
        else
            if (DropdownPechRightOrLeft.value == 1)
        {
            
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechRightOrLeft>", "</PechRightOrLeft>"), "<PechRightOrLeft>" + "Right" + "</PechRightOrLeft>");

        }

    }

    public void ManageUgolLampsInParilka()
    {
        UglovLampLeftTopInParilka.transform.position = new Vector3(TolschinaStenKarkasa, 1.85f, TolschinaStenKarkasa);
        UglovLampLeftTopInParilka.transform.eulerAngles = new Vector3(0, -90, 0);
        UglovLampRightTopInParilka.transform.position = new Vector3(TolschinaStenKarkasa, 1.85f, (BanyaWidth / 100f) - TolschinaStenKarkasa);
      
        if (PechRightOrLeft == "Left")
        {
            UglovLampBottomInParilka.transform.position = new Vector3(TolschinaStenKarkasa + (XPosOfPeregorodkaParilnogo / 100f), 1.85f, (BanyaWidth / 100f) - TolschinaStenKarkasa);
            UglovLampBottomInParilka.transform.eulerAngles = new Vector3(0, -270, 0);
        }

        else
            if (PechRightOrLeft == "Right")
        {
            UglovLampBottomInParilka.transform.position = new Vector3(TolschinaStenKarkasa + (XPosOfPeregorodkaParilnogo / 100f), 1.85f, TolschinaStenKarkasa);
            UglovLampBottomInParilka.transform.eulerAngles = new Vector3(0, -180, 0);
        }

    }
    public void ManageOptionPechRightOrLeft(string option)
    {
        if (option == "Right")
        {
            Pech.transform.position = new Vector3(Pech.transform.position.x, Pech.transform.position.y, 1.939f);
            if (PechIndoorOrOutdoor == "Outdoor")
                Pech.transform.eulerAngles = new Vector3(-90, -90, 0);
            PechRightOrLeft = "Right";
            DropdownPechRightOrLeft.value = 1;
        }
        else
            if (option == "Left")
        {
            Pech.transform.position = new Vector3(Pech.transform.position.x, Pech.transform.position.y, 0.37f);
            if (PechIndoorOrOutdoor == "Outdoor")
                Pech.transform.eulerAngles = new Vector3(-90, 90, 0);
            PechRightOrLeft = "Left";
            DropdownPechRightOrLeft.value = 0;
        }

    }
    public void SetOptionIndoorOrOutDoorPech()
    {


        if (PechOutdoorToggle.isOn)
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorOrOutdoor>", "</PechIndoorOrOutdoor>"), "<PechIndoorOrOutdoor>" + "Outdoor" + "</PechIndoorOrOutdoor>");





            





        }
        else
        {
        

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<PechIndoorOrOutdoor>", "</PechIndoorOrOutdoor>"), "<PechIndoorOrOutdoor>" + "Indoor" + "</PechIndoorOrOutdoor>");
        }


    }
    public void ManageOptionIndoorOrOutDoorPech(string option)
    {
        if (option == "Indoor")
        {
            Pech.transform.eulerAngles = new Vector3(-90, 0, 0);
            PechIndoorOrOutdoor = "Indoor";
            PechOutdoorToggle.isOn = false;






        }
        else
        {
            if (option == "Outdoor")
            {
                if (PechRightOrLeft == "Right")
                {
                    Pech.transform.eulerAngles = new Vector3(-90, -90, 0);
                    PechIndoorOrOutdoor = "Outdoor";
                    PechOutdoorToggle.isOn = true;
                }
                else
                {
                    {
                        Pech.transform.eulerAngles = new Vector3(-90, 90, 0);
                        PechIndoorOrOutdoor = "Outdoor";
                        PechOutdoorToggle.isOn = true;
                    }
                }
            }
        }






   









    }
    public void SetOptionMoechnayaDlina()
    {
        if (UIMoechnayaDlinaIF.text.Length != 0)
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MoechnayaDlina>", "</MoechnayaDlina>"), "<MoechnayaDlina>" + int.Parse(UIMoechnayaDlinaIF.text) + "</MoechnayaDlina>");
        else
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MoechnayaDlina>", "</MoechnayaDlina>"), "<MoechnayaDlina>" + 0 + "</MoechnayaDlina>");
    }
    public void ManageOptionMoechnayaDlina(string option)
    {

        XPosOfPeregorodkaMoechnoi = XPosOfPeregorodkaParilnogo + int.Parse(option);
        UIMoechnayaDlinaIF.text = option;
        Peregorodka2.GetComponent<WallScript>().RefreshWall();

    }
        
    public void SetOptionRasstOtParilkiToStenkaDusha()
    {
        if (UIRasstOtParilkiToStenkaDushaIF.text.Length != 0)
        { 
         //if (int.Parse(UIRasstOtParilkiToStenkaDushaIF.text)+XPosOfPeregorodkaParilnogo<BanyaLength )
                CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RasstOtParilkiToStenkaDusha>", "</RasstOtParilkiToStenkaDusha>"), "<RasstOtParilkiToStenkaDusha>" + int.Parse(UIRasstOtParilkiToStenkaDushaIF.text) + "</RasstOtParilkiToStenkaDusha>");
            // else
            //        CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<RasstOtParilkiToStenkaDusha>", "</RasstOtParilkiToStenkaDusha>"), "<RasstOtParilkiToStenkaDusha>" + (BanyaLength - XPosOfPeregorodkaParilnogo-100*TolschinaStenKarkasa*3) + "</RasstOtParilkiToStenkaDusha>");
            //
        }
     }
    public void ManageOptionRasstOtParilkiToStenkaDusha(int option)
    {

        //Debug.Log(option+ "RasstOtParilkiToStenkaDusha");
        //Debug.Log(XPosOfPeregorodkaParilnogo + "XPosOfPeregorodkaParilnogo");
        
        RasstOtParilkiToStenkaDusha = option;
       

       
        XposOfStenkaDusha = XPosOfPeregorodkaParilnogo + RasstOtParilkiToStenkaDusha+TolschinaStenKarkasa*100f*2.5f;

        if (XposOfStenkaDusha >=  BanyaLength- 0.5f*TolschinaStenKarkasa*100f) //StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x*100f)
        {
            
            //XposOfStenkaDusha = StenaShirina2_SDveryu.GetComponent<WallScript>().WallCube1.transform.position.x * 100f;
            XposOfStenkaDusha = BanyaLength-0.5f*TolschinaStenKarkasa*100f;

            
            UIRasstOtParilkiToStenkaDushaIF.text=( (XposOfStenkaDusha - XPosOfPeregorodkaParilnogo) - TolschinaStenKarkasa*100f*2.5f).ToString();
            
            //SetOptionRasstOtParilkiToStenkaDusha();
            StenkaDusha.SetActive(false);

            RasstOtParilkiToStenkaDusha = int.Parse(UIRasstOtParilkiToStenkaDushaIF.text);
            StenkaDusha.GetComponent<WallScript>().RefreshWall();

            StenkaDushaIn2SectionBanyaTheSamePosAsStenaSDveryu = true;
        }

        else {
           // Debug.Log(":меньше");
            StenkaDushaIn2SectionBanyaTheSamePosAsStenaSDveryu = false;
            //StenkaDusha.SetActive(true);
            UIRasstOtParilkiToStenkaDushaIF.text = option.ToString();
            
            StenkaDusha.GetComponent<WallScript>().RefreshWall();
        }

       
    }






    









    public void SetOptionForDushNalichie()
    {
        if (ToggleNalichieDusha.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushNalichie>", "</DushNalichie>"), "<DushNalichie>" + "DushEnabled" + "</DushNalichie>");


        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<DushNalichie>", "</DushNalichie>"), "<DushNalichie>" + "NoDush" + "</DushNalichie>");
        }
    }

   


        

    public void ManageOptionForDushNalichie(string option)
    {
        if (option == "NoDush")
        {
            Dush.SetActive(false);

            DushNalichie = "NoDush";
            ToggleNalichieDusha.isOn = false;
            StenkaDusha.SetActive(false);
            UIRasstOtParilkiToStenkaDushaIF.gameObject.SetActive(false);
            //тут dushwa


            foreach (Renderer r in DushWaterPos1.GetComponentsInChildren<Renderer>())
            {  r.enabled = false;  }
            foreach (Renderer r in DushWaterPos2.GetComponentsInChildren<Renderer>())
            { r.enabled = false; }
            foreach (Renderer r in DushWaterPos3.GetComponentsInChildren<Renderer>())
            { r.enabled = false; }

        }
        else
        if (option == "DushEnabled")
        {

            foreach (Renderer r in DushWaterPos1.GetComponentsInChildren<Renderer>())
            {  r.enabled = true; }
            foreach (Renderer r in DushWaterPos2.GetComponentsInChildren<Renderer>())
            { r.enabled = true; }
            foreach (Renderer r in DushWaterPos3.GetComponentsInChildren<Renderer>())
            { r.enabled = true; }




            Dush.SetActive(true);
            DushNalichie = "DushEnabled";
            ToggleNalichieDusha.isOn = true;
            
            if (SectionCount==3)
            {
                StenkaDusha.SetActive(false);
                UIRasstOtParilkiToStenkaDushaIF.gameObject.SetActive(false);
            }
            else

            {
                StenkaDusha.SetActive(true);
                UIRasstOtParilkiToStenkaDushaIF.gameObject.SetActive(true);
            }
        }

        if (PechIndoorOrOutdoor=="Indoor")
        {

            ToggleDushPos3.gameObject.SetActive(false);
            ToggleDushPos4.gameObject.SetActive(false);
        }
        else
        {

            ToggleDushPos3.gameObject.SetActive(true);
            ToggleDushPos4.gameObject.SetActive(true);
        }
    }

    public void UIManageBirOrMalStolDependingOfBanyaLengthDropdown()
    {
      if (StandartDropdown.value<=2)
        {
            ToggleNalichieBigStola.gameObject.SetActive(false);
            ToggleNalichieMalStola.gameObject.SetActive(true);
               
        }
      else
        {
            ToggleNalichieBigStola.gameObject.SetActive(true);
            ToggleNalichieMalStola.gameObject.SetActive(false);
             
        }


 

    }

    public void SetOptionForBigStolNalichie()
    {
        if (ToggleNalichieBigStola.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolNalichie>", "</BigStolNalichie>"), "<BigStolNalichie>" + "BigStolEnabled" + "</BigStolNalichie>");


        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<BigStolNalichie>", "</BigStolNalichie>"), "<BigStolNalichie>" + "NoBigStol" + "</BigStolNalichie>");
        }
    }
    public void ManageOptionForBigStolNalichie(string option)
    {
        if (option == "NoBigStol")
        {
            BigStolGO.SetActive(false);

            BigStolNalichie = "NoBigStol";
            ToggleNalichieBigStola.isOn = false;
            UIManageBirOrMalStolDependingOfBanyaLengthDropdown();
        }
        if (option == "BigStolEnabled")
        {
            
            BigStolGO.SetActive(true);
            
            BigStolNalichie = "BigStolEnabled";
            ToggleNalichieBigStola.isOn = true;
            UIManageBirOrMalStolDependingOfBanyaLengthDropdown();


            if ((StandartDropdown.value == 0) || (StandartDropdown.value == 1) || (StandartDropdown.value == 2))
            {
                BigStolGO.SetActive(false);
                
            }
            else
                BigStolGO.SetActive(true);

        }

        
    }


    public void SetOptionForMalStolNalichie()
    {
        if (ToggleNalichieMalStola.isOn)
        {

            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolNalichie>", "</MalStolNalichie>"), "<MalStolNalichie>" + "MalStolEnabled" + "</MalStolNalichie>");


        }
        else
        {
            CodeOfBanya = CodeOfBanya.Replace(GetStringWithSomeStartAndEnd(CodeOfBanya, "<MalStolNalichie>", "</MalStolNalichie>"), "<MalStolNalichie>" + "NoMalStol" + "</MalStolNalichie>");
        }
    }


    public void ManageOptionForMalStolNalichie(string option)
    {
        if (option == "NoMalStol")
        {
            MalStolGO.SetActive(false);

            MalStolNalichie = "NoMalStol";
            ToggleNalichieMalStola.isOn = false;
            UIManageBirOrMalStolDependingOfBanyaLengthDropdown();
        }
        if (option == "MalStolEnabled")
        {
            MalStolGO.SetActive(true);
            MalStolNalichie = "MalStolEnabled";
            ToggleNalichieMalStola.isOn = true;
            UIManageBirOrMalStolDependingOfBanyaLengthDropdown();

            if ((StandartDropdown.value == 3) || (StandartDropdown.value == 4) || (StandartDropdown.value == 5) || (StandartDropdown.value == 6) || (StandartDropdown.value == 7) || (StandartDropdown.value == 8))
            {
                MalStolGO.SetActive(false);
               
            }
            else
                MalStolGO.SetActive(true);

        }
    }


    #endregion UISolved




    public void UICloseDushSelectionPanel()
    {

        UIDushSelectionPanel.SetActive(false);
    }
    public void UICloseSlivPosSelectionPanel()
    {

        SlivPosSoglasovaniePanel.SetActive(false);
    }
    public void UIClose220PosSelectionPanel()
    {

        _220PosSoglasovaniePanel.SetActive(false);
    }
    public void UIOpen220PosSelectionPanel()
    {

           

        _220PosSoglasovaniePanel.SetActive(true);
    }
    public void UICloseDushWaterPosSelectionPanel()
    {

        DushWaterPosSoglasovaniePanel.SetActive(false);
    }
    public void UIOpenDushWaterPosSelectionPanel()
    {



        DushWaterPosSoglasovaniePanel.SetActive(true);
    }
    public void UIOpenSlivPosSelectionPanel()
    {

        if (UIDropdownForSectionCount.value == 0)
        {
            ImageSchemeSlivPos3.gameObject.SetActive(true);
            ImageSchemeSlivPos5.gameObject.SetActive(false);
            ToggleSlivePos1.gameObject.SetActive(true);
            ToggleSlivePos2.gameObject.SetActive(true);
            ToggleSlivePos3.gameObject.SetActive(true);
            ToggleSlivePos4.gameObject.SetActive(false);
            ToggleSlivePos5.gameObject.SetActive(false);

        }
        else
        {
            if (UIDropdownForSectionCount.value == 1)
            {
                ImageSchemeSlivPos3.gameObject.SetActive(false);
                ImageSchemeSlivPos5.gameObject.SetActive(true);
                ToggleSlivePos1.gameObject.SetActive(true);
                ToggleSlivePos2.gameObject.SetActive(true);
                ToggleSlivePos3.gameObject.SetActive(true);
                ToggleSlivePos4.gameObject.SetActive(true);
                ToggleSlivePos5.gameObject.SetActive(true);
            }
        }




        SlivPosSoglasovaniePanel.SetActive(true);
    }
    public void UICloseSkameikiPosVMoechnoiSelectionPanel()
    {
        
        UISelectSkameikiPosVMoechnoiPanel.SetActive(false);
    }
    public void UIOpenDushSelectionPanel()
    {

        UIDushSelectionPanel.SetActive(true);
    }
    public void UIOpenSkameikiSelectVMoechnoiPanel()
    {

        UISelectSkameikiPosVMoechnoiPanel.SetActive(true);
    }
    public void UIManageTogglesInSkamVMoechnoiSelectionPanel()
    {
        if (UIDropdownForSectionCount.value == 1)
        {
            if (PechIndoorOrOutdoor == "Indoor")
            {
                if (PechRightOrLeft == "Right")  //если печь Indoor справа
                {
                    Image_3sectionPecnIndoorRightSkam.SetActive(false);
                    Image_3sectionPecnIndoorLeftSkam.SetActive(true);
                    Image_3sectionPechOutdoorSkamsObschaya.SetActive(false);

                    ToggleSelectRightSkamIndoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectLeftSkamIndoorPech3Section.gameObject.SetActive(true);



                 ToggleSelectHorRightSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectHorRightSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectHorLeftSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectHorLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(false);

                 ToggleSelectVerticalRightSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                 ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                }
                else  //если печь слева
                if (PechRightOrLeft == "Left")  //если печь Indoor слева
                {
                    Image_3sectionPecnIndoorRightSkam.SetActive(true);
                    Image_3sectionPecnIndoorLeftSkam.SetActive(false);
                    Image_3sectionPechOutdoorSkamsObschaya.SetActive(false);

                    ToggleSelectRightSkamIndoorPech3Section.gameObject.SetActive(true);
                    ToggleSelectLeftSkamIndoorPech3Section.gameObject.SetActive(false);


                    ToggleSelectHorRightSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectHorRightSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectHorLeftSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectHorLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(false);

                    ToggleSelectVerticalRightSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.gameObject.SetActive(false);
                    ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(false);
                }
            }
            
          else  //если печь Outdoor
            {

                Image_3sectionPecnIndoorRightSkam.SetActive(false);
                Image_3sectionPecnIndoorLeftSkam.SetActive(false);
                Image_3sectionPechOutdoorSkamsObschaya.SetActive(true);


                ToggleSelectRightSkamIndoorPech3Section.gameObject.SetActive(false);
                ToggleSelectLeftSkamIndoorPech3Section.gameObject.SetActive(false);


                ToggleSelectHorRightSkamNizOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectHorRightSkamVerhOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectHorLeftSkamNizOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectHorLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(true);

                ToggleSelectVerticalRightSkamNizOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectVerticalRightSkamVerhOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectVerticalLeftSkamNizOutdoorPech3Section.gameObject.SetActive(true);
                ToggleSelectVerticalLeftSkamVerhOutdoorPech3Section.gameObject.SetActive(true);
            }



        }
        else
        {

            /*UIMoechnayaDlinaIF.gameObject.SetActive(false);
            UIMoechnayaDlinaText.gameObject.SetActive(false);

            UIDushPosScheme_2section.SetActive(true);
            UIDushPosScheme_3sectionPechIndoor.SetActive(false);
            UIDushPosScheme_3sectionPechOutdoor.SetActive(false);

            ToggleDushPos1.gameObject.SetActive(true);
            ToggleDushPos2.gameObject.SetActive(true);
            ToggleDushPos3.gameObject.SetActive(false);
            ToggleDushPos4.gameObject.SetActive(false);*/

        }
    }
    public void UIEnableDisableBtnSelectionDushPos()
    {
        if (ToggleNalichieDusha.isOn)
        {
            UIBtnSelectionDushPos.SetActive(true);
            BtnSoglasovatDushWater.SetActive(true);
        }
        else
        {
            UIBtnSelectionDushPos.SetActive(false);
            BtnSoglasovatDushWater.SetActive(false);
        }
    }
    public void UIEnableDisableDlinaMoechnogoIF()
    {
        if (UIDropdownForSectionCount.value == 1)
        {
            UIMoechnayaDlinaIF.gameObject.SetActive(true);
            UIMoechnayaDlinaText.gameObject.SetActive(true);

            
           // UIDushPosScheme_2section.SetActive(false);
            //ToggleDushPos1.gameObject.SetActive

             
        }
        else
        {
            UIMoechnayaDlinaIF.gameObject.SetActive(false);
            UIMoechnayaDlinaText.gameObject.SetActive(false);

         //   UIDushPosScheme_2section.SetActive(true);
            //UIDushPosScheme_3sectionPechIndoor.SetActive(false);
            //UIDushPosScheme_3sectionPechOutdoor.SetActive(false);

         

        }
    }
    public void UIEnableDisableDushPosToggles()
    {
        if (UIDropdownForSectionCount.value == 1)  //3 секции
        {
            UIMoechnayaDlinaIF.gameObject.SetActive(true);
            UIMoechnayaDlinaText.gameObject.SetActive(true);



            UIDushPosScheme_2sectionIndoor.SetActive(false);
            UIDushPosScheme_2sectionOutdoor.SetActive(false);
            //ToggleDushPos1.gameObject.SetActive
            print("PechIndoorOrOutdoor: "+PechIndoorOrOutdoor);
            if (PechIndoorOrOutdoor == "Indoor")
            {
                UIDushPosScheme_3sectionPechIndoor.SetActive(true);
                UIDushPosScheme_3sectionPechOutdoor.SetActive(false);
                ToggleDushPos1.gameObject.SetActive(true);
                ToggleDushPos2.gameObject.SetActive(true);
                ToggleDushPos3.gameObject.SetActive(false);
                ToggleDushPos4.gameObject.SetActive(false);
            }


            if (PechIndoorOrOutdoor == "Outdoor")
            {
                UIDushPosScheme_3sectionPechIndoor.SetActive(false);
                UIDushPosScheme_3sectionPechOutdoor.SetActive(true);


                ToggleDushPos1.gameObject.SetActive(true);
                ToggleDushPos2.gameObject.SetActive(true);
                ToggleDushPos3.gameObject.SetActive(true);
                ToggleDushPos4.gameObject.SetActive(true);
            }

        }
        else
        {
            UIMoechnayaDlinaIF.gameObject.SetActive(false);
            UIMoechnayaDlinaText.gameObject.SetActive(false);

            
            UIDushPosScheme_3sectionPechIndoor.SetActive(false);
            UIDushPosScheme_3sectionPechOutdoor.SetActive(false);



            if (PechIndoorOrOutdoor == "Outdoor")
            {
                UIDushPosScheme_2sectionIndoor.SetActive(false);
                UIDushPosScheme_2sectionOutdoor.SetActive(true);

                ToggleDushPos1.gameObject.SetActive(true);
                ToggleDushPos2.gameObject.SetActive(true);
                ToggleDushPos3.gameObject.SetActive(true);
                ToggleDushPos4.gameObject.SetActive(true);

            }


            if (PechIndoorOrOutdoor == "Indoor")
            {
                UIDushPosScheme_2sectionIndoor.SetActive(true);
                UIDushPosScheme_2sectionOutdoor.SetActive(false);

                ToggleDushPos1.gameObject.SetActive(true);
                ToggleDushPos2.gameObject.SetActive(true);
                ToggleDushPos3.gameObject.SetActive(false);
                ToggleDushPos4.gameObject.SetActive(false);
            }



            

        }
    }
    public void UIEnableDisableBtnSelectPosSkameikiVMoechnoi()
    {
        /*if (UIDropdownForSectionCount.value == 0)  //2 секции
        {
            UIBtnSelectSkameikiPosVMoechnoi.gameObject.SetActive(false);
            UIBtnSelectSkameikiPosVMoechnoi.gameObject.SetActive(false);
        }
        else
            if (UIDropdownForSectionCount.value == 1)  //3 секции
        {
            UIBtnSelectSkameikiPosVMoechnoi.gameObject.SetActive(true);
            UIBtnSelectSkameikiPosVMoechnoi.gameObject.SetActive(true);
        }*/
    }
    public void UIDlinaBaniCheckFromDropdownBolee5MetrovForSectionCount()

    {
        if(StandartDropdown.value>=2)  //секции может быть 2 или 3
        {
            UIDropdownForSectionCount.gameObject.SetActive(true);
            UITextInfoForSectionCount.gameObject.SetActive(false);

            UIEnableDisableDlinaMoechnogoIF();


           
        }
        else              //секции только 2
        {
            UIDropdownForSectionCount.gameObject.SetActive(false);
            UITextInfoForSectionCount.gameObject.SetActive(true);
            UIDropdownForSectionCount.value = 0;
            UIMoechnayaDlinaIF.gameObject.SetActive(false);
            UIMoechnayaDlinaText.gameObject.SetActive(false);

           

        }

    }

    // Use this for initialization
    void Start()
    {
        BanyaLengthInfo.text = BanyaLength.ToString();
        BanyaWidthInfo.text = BanyaWidth.ToString();

    }

    // Update is called once per frame
    void Update()
    {
         
    }

   
      

   


    public string GetStringWithSomeStartAndEnd(string inputString, string _openString, string _closeString)
    {

        if (inputString.Contains(_openString) && inputString.Contains(_closeString))
        {
            /*  string stringFromOpenString = inputString.Remove(0, inputString.IndexOf(_openString) + _openString.Length);




              string stringAfterSecondString = stringFromOpenString.Substring(stringFromOpenString.IndexOf(_closeString));



              //inputString = inputString.
              inputString = inputString.Substring(inputString.IndexOf(_openString) + _openString.Length, stringFromOpenString.Length - stringAfterSecondString.Length);*/



            inputString = inputString.Substring(inputString.IndexOf(_openString));
            inputString = inputString.Substring(0, inputString.LastIndexOf(_closeString) + _closeString.Length);  //(                  inputString.IndexOf(_openString));



            return inputString;
        }
        else

        {

            return "";

        }


    }
  
    public void RebuildAllBanya()
    {

        AllWalls = GameObject.FindGameObjectsWithTag("WallScripted");

        
        foreach (GameObject wall in AllWalls)
        {

            
                wall.GetComponent<WallScript>().RefreshWall();


        }


        StenkaDusha.GetComponent<WallScript>().RefreshWall();

    }





    public void RebuildAllBanya2_NeedToReInitializeWindowsAdding()
    {

        AllWalls = GameObject.FindGameObjectsWithTag("WallScripted");


        foreach (GameObject wall in AllWalls)
        {
            
                wall.GetComponent<WallScript>().RefreshWindowsAdded();


        }
        StenkaDusha.GetComponent<WallScript>().RefreshWall();
    }






    

}
