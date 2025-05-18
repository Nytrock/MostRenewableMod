namespace EverythingRenewableNow.Systems.Recipes {
    internal struct StatueRecipeInfo {
        public int ItemID { get; private set; }
        public int StatueID { get; private set; }
        public int ItemsCount { get; private set; }
        public bool RequireGraveyard { get; private set; }
        public bool IsItemGroup { get; private set; }

        public StatueRecipeInfo(int itemID, int statueID, int itemsCount = 1, bool requireGraveyard = false, bool itemIsGroup = false) {
            ItemID = itemID;
            StatueID = statueID;
            ItemsCount = itemsCount;
            RequireGraveyard = requireGraveyard;
            IsItemGroup = itemIsGroup;
        }
    }
}
