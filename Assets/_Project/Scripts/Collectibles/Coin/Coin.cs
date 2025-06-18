public class Coin : Collectible
{
    public override void Accept(ICollectibleVisitor visitor)
    {
        if (visitor is ICoinVisitor coinVisitor)
            coinVisitor.Visit(this);
    }
}