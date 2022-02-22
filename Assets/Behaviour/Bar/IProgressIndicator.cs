internal interface IProgressIndicator
{
    void Hide(bool hide);
    void SetProgress(float value);
}