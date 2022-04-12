﻿using System.Collections.Generic;
using System.Linq;

public class VisitorsDishAccepter
{
    private List<Visitor> visitors;

    public VisitorsDishAccepter(List<Visitor> _visitors)
    {
        visitors = _visitors;
        SelectableDish.OnSelectDish += AcceptDishInVisitor;
    }

    private void AcceptDishInVisitor(DishData data)
    {
        Visitor visitor = FindVisitorHasDish(data);
        if (visitor != null)
        {
            visitor.AcceptDish(data);
        }
    }

    private Visitor FindVisitorHasDish(DishData data)
    {
        Visitor[] visitorsHasDish = visitors.Where(visitor => visitor.VisitorDishes.ContainsValue(data)).ToArray();
        if (visitorsHasDish.Length != 0)
        {
            return visitorsHasDish[RandomNumberGenerator.Generate(visitorsHasDish.Length)];
        }
        return null;
    }
}
