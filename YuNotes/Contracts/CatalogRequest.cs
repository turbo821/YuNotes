namespace YuNotes.Contracts
{
    public record CatalogRequest(Guid? GroupId, string? SearchTitle, SortState SortOrder = SortState.EditDesc);
}
