/******************************************************************************
 * 
 * File: BehaviorTreeSplitView.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of experimental SplitView for editing behavior trees.
 *  
 ******************************************************************************/
using UnityEngine.UIElements;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Extension of experimental SplitView for editing behavior trees.
    /// </summary>
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits> { }
        public SplitView()
        {

        }
    }
}