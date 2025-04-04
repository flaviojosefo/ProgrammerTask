public class Battery : ItemBase
{
    public override void PickUp()
    {
        print("Picked up a Battery!");
        Destroy(gameObject);
    }
}
