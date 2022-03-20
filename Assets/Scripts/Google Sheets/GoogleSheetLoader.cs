using System;
using UnityEngine;
using Utils;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    public static GoogleSheetLoader Instance;
    public event Action<HeroSheetsData> OnProcessData;

    [SerializeField] private string _sheetId;
    [SerializeField] private HeroSheetsData _data;

    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Start()
    {
        Instance = this;
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        DownloadTable();
    }

    public HeroSheetsData GetTable()
    {
        return _data;
    }

    private void DownloadTable()
    {
        _cvsLoader.DownloadTable(_sheetId, OnRawCVSLoaded);
    }

    private void OnRawCVSLoaded(string rawCVSText)
    {
        _data = _sheetProcessor.ProcessData(rawCVSText);
        OnProcessData?.Invoke(_data);
    }
}