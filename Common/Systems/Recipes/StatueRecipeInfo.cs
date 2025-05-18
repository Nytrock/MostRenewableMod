namespace EverythingRenewableNow.Common.Systems.Recipes {
    public struct StatueRecipeInfo(int itemID, int statueID, int itemsCount = 1, bool requireGraveyard = false, bool itemIsGroup = false) {
        public int ItemID { get; private set; } = itemID;
        public int StatueID { get; private set; } = statueID;
        public int ItemsCount { get; private set; } = itemsCount;
        public bool RequireGraveyard { get; private set; } = requireGraveyard;
        public bool IsItemGroup { get; private set; } = itemIsGroup;
    }
}
