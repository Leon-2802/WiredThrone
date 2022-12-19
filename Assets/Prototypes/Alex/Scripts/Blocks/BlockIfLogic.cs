#nullable enable

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BlockIfLogic<T>
{
    [SerializeField] private BlockInfo<T>? _left, _right;
    [SerializeField] private Operator? _operator;
    [SerializeField] private InputField _leftInput;
    [SerializeField] private InputField _rightInput;

    public BlockIfLogic(BlockInfo<T>? left, Operator? op, BlockInfo<T>? right)
    {
        _left = left;
        _operator = op;
        _right = right;
    }

    public void SetLeft(BlockInfo<T> block)
    {
        _left = block;
    }

    public void SetRight(BlockInfo<T> block)
    {
        _right = block;
    }

    public void SetOperator(Operator op)
    {
        _operator = op;
    }

    public string GetOperator {
        get {return _operator.ToString();}
    }

    public void SetOperatorFromString(string op)
    {
        switch (op)
        {
            case "==":
                _operator = Operator.Equal;
                break;
            case "!=":
                _operator = Operator.NotEqual;
                break;
            case "<":
                _operator = Operator.LessThan;
                break;
            case "<=":
                _operator = Operator.LessOrEqual;
                break;
            case ">":
                _operator = Operator.MoreThan;
                break;
            case ">=":
                _operator = Operator.MoreOrEqual;
                break;
        }
    }

    public bool IsSet()
    {
        if (_left != null && _operator != null && _right != null)
        {
            return true;
        }
        return false;
    }

    public bool ParseInput()
    {
        // Blocktypes should be equal.
        if (_left != null && _right != null && _left.Type == _right.Type)
        {
            int c = Comparer<T>.Default.Compare(_left.Value, _right.Value);
            bool result = false;
            switch (_operator)
            {
                // <
                case Operator.LessThan:
                    result = (c < 0) ? true : false;
                    break;

                // <=
                case Operator.LessOrEqual:
                    if (c < 0 || c == 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                // >
                case Operator.MoreThan:
                    result = (c > 0) ? true : false;
                    break;

                // >=
                case Operator.MoreOrEqual:
                    if (c > 0 || c == 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    break;

                // ==
                case Operator.Equal:
                    result = (c == 0) ? true : false;
                    break;

                // !=
                case Operator.NotEqual:
                    result = (c != 0) ? true : false;
                    break;
            }
            return result;
        }
        return false;
    }
}