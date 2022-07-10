using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlockIf<T> : Block<bool>
{

    [SerializeField] private BlockInfo<T>? _left, _right;
    [SerializeField] private Operator? _operator;

    public BlockIf(BlockInfo<T>? left, Operator? op, BlockInfo<T>? right)
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

    public void SetOperator(Operator op) {
        _operator = op;
    }

    public bool IsSet() {
        if (_left != null && _operator != null && _right != null ) {
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
                    result = false;
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
                    result = false;
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
