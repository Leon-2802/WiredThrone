using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BlockIf
{
    private BlockIfLogic<int> _blockIfLogic = new BlockIfLogic<int>(null, null, null);
    public BlockIfLogic<int> Logic {
        get { return _blockIfLogic; }
    }

    public BlockIf(BlockInfo<int> leftBlock, Operator op, BlockInfo<int> rightBlock) {
        _blockIfLogic = new BlockIfLogic<int>(leftBlock, op, rightBlock);
    }

    public BlockIf(BlockInfo<float> leftBlock, Operator op, BlockInfo<float> rightBlock) {

    }

}
