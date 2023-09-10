using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public abstract string Name { get; set; }
    public abstract string Description { get; set; }
}
public class ActiveItem:Item
{
    public override string Name { get; set; }
    public override string Description { get; set; }
    public Action mainAction { get; set; }
    public Action alternativeAction { get; set; }
}
public abstract class ArmorItem:Item
{
    public List<Resistance> resistances { get; set; } = new List<Resistance>();
      
}
public class Resistance
{
    public float ResitanceValue { get; set; }
    public DamageType damageType { get; set; }
}
public class Action
{
    public virtual void DoAction()//Creature itemUser)
    {

    }
    public Action()
    {

    }
}

public class AttackAction:Action
{
    public override void DoAction()//Creature itemUser)
    {
        //itemUser.Attack(this);
    }
    public float AttackValue { get; set; }
    public DamageType damageType { get; set; }
}
