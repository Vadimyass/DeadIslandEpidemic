using System;
using UnityEngine;
using Utils;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    public static GoogleSheetLoader Instance;
    public event Action<HeroSheetsData> OnProcessData;

    [SerializeField] private string _sheetId;
    [SerializeField] private string _skillsListId;
    [SerializeField] private string _heroesListId;
    [SerializeField] private HeroSheetsData _data;
    [SerializeField] private HeroSkillsSheetsData _skillsData;
    [SerializeField] private bool isSkills;

    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Start()
    {
        Instance = this;
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        DownloadHeroesTable();
        DownloadSkillTable();
    }

    public HeroSheetsData GetTable()
    {
        return _data;
    }
    public HeroSkillsSheetsData GetSkillTable()
    {
        return _skillsData;
    }

    private void DownloadHeroesTable()
    {
        _cvsLoader.DownloadTable(_sheetId, _heroesListId, OnRawCVSLoaded);
    }
    private void DownloadSkillTable()
    {
        _cvsLoader.DownloadTable(_sheetId, _skillsListId, OnRawSkillCVSLoaded);
    }

    private void OnRawCVSLoaded(string rawCVSText)
    {
        _data = _sheetProcessor.ProcessData(rawCVSText);
    }
    private void OnRawSkillCVSLoaded(string rawCVSText)
    {
        _skillsData = _sheetProcessor.ProcessSkillData(rawCVSText);
    }
    
}