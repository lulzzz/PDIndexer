﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls
{
    public partial class BoundControl : UserControl
    {

        public bool SkipEvent { get; set; } = false;
        public Crystal Crystal { get; set; } = null;

        public event EventHandler BoundsChanged;

        public BoundControl()
        {
            InitializeComponent();
        }


        public Bound GetFromInterface()
        {
            return new Bound(true, Crystal, numericBoxH.ValueInteger, numericBoxK.ValueInteger, numericBoxL.ValueInteger,
                checkBoxEquivalency.Checked, numericBoxDistance.Value, colorControl.Argb);
        }

        public void SetToInterface(Bound b)
        {
            numericBoxH.Value = b.Index.H;
            numericBoxK.Value = b.Index.K;
            numericBoxL.Value = b.Index.L;

            checkBoxEquivalency.Checked = b.Equivalency;

            numericBoxDistance.Value = b.Distance;

            colorControl.Color = Color.FromArgb(b.ColorArgb);
        }




        #region データベース操作
        /// <summary>
        /// データベースにbondsを追加する
        /// </summary>
        /// <param name="bonds"></param>
        public void Add(Bound bounds)
        {
            if (bounds != null)
            {
                dataSet.DataTableBound.Add(bounds);
                BoundsChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// データベースに原子を追加する
        /// </summary>
        /// <param name="bonds"></param>
        public void AddRange(IEnumerable<Bound> bounds)
        {
            if (bounds != null)
            {
                foreach (var b in bounds)
                    dataSet.DataTableBound.Add(b);
                BoundsChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// データベースのi番目の原子を削除
        /// </summary>
        /// <param name="i"></param>
        public void Delete(int i)
        {
            dataSet.DataTableBound.Remove(i);
            BoundsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースのi番目の原子を置換
        /// </summary>
        /// <param name="bonds"></param>
        /// <param name="i"></param>
        public void Replace(Bound bounds, int i)
        {
            dataSet.DataTableBound.Replace(bounds, i);
            BoundsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベースの原子を全て削除する
        /// </summary>
        public void Clear()
        {
            dataSet.DataTableBound.Clear();
            BoundsChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// データベース中の全ての原子を取得
        /// </summary>
        /// <returns></returns>
        public Bound[] GetAll() => dataSet.DataTableBound.GetAll();

        #endregion


        #region 追加/削除/置換 ボタン

        /// <summary>
        /// 追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            var bound = GetFromInterface();
            if (bound != null)
            {
                Add(bound);
                bindingSource.Position = bindingSource.Count - 1;
            }
        }

        /// <summary>
        /// 変更ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChange_Click(object sender, System.EventArgs e)
        {
            var pos = bindingSource.Position;
            if (pos >= 0)
            {
                Replace(GetFromInterface(), pos);
                bindingSource.Position = pos;
            }
        }

        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, System.EventArgs e)
        {
            int pos = bindingSource.Position;
            if (pos >= 0)
            {
                SkipEvent = true;//bindingSourceAtoms_PositionChangedが呼ばれるのを防ぐ
                Delete(pos);
                bindingSource.Position = bindingSource.Count > pos ? pos : pos - 1;//選択列を選択しなおす
                SkipEvent = false;
            }
        }

        #endregion

        //選択Atomが変更されたとき
        private void bindingSource_PositionChanged(object sender, System.EventArgs e)
        {
            if (SkipEvent) return;

            if (bindingSource.Position >= 0 && bindingSource.Count > 0)
                SetToInterface(dataSet.DataTableBound.Get(bindingSource.Position));
        }



    }
}