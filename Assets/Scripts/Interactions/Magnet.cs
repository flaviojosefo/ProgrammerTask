public class Magnet : ItemBase
{
    public override void PickUp()
    {
        print("Picked up a Magnet!");
        Destroy(gameObject);
    }
}
