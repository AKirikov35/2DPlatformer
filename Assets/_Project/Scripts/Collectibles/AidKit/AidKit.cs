public class AidKit : Collectible
{
    public override void Accept(ICollectibleVisitor visitor)
    {
        if (visitor is IAidKitVisitor aidKitVisitor)
            aidKitVisitor.Visit(this);
    }
}