
/// <summary>
/// //////////////////////////////
/// THESE ARE ONLY FOR ENEMY//////
/// //////////////////////////////
/// </summary>

//==================Basic========================
interface IMovable
{
    public void Move();
}
interface IDeathable
{
    public void Death();
}
interface IDetecable
{
    public void Detect();
}
interface IDamagable
{
    public void TakeDamage(float damage);
}
interface IAttackable
{
    public void Attack();
}
interface ISkillable
{
    public void Skill();
}
//===============================================




//=================Combat State==================
interface IBurnable//PlankOnFire
{
    public void Burn();
}
interface IStunable//Hammer
{
    public void Stun();
}
interface IFreezable//CrystalSword
{
    public void Freeze();
}
//===============================================





