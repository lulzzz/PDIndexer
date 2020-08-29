using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Crystallography;
using Crystallography.Controls;

namespace PDIndexer
{
    public partial class FormLPO : Form
    {
        public FormMain formMain;
        public FormLPO()
        {
            InitializeComponent();
        }


        private void FormLPO_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formMain.toolStripButtonLPO.Checked = false;
        }

        
        List<List<(int H, int K, int L)>> listIndices;
        List<string> listStrHkl;
        List<double> listTwoTheta;
        List<double> listAngle;
        List<List<double>> obsDividedIntensity;
        double[][] calcDividedIntensity;
        double[][] calcDividedIntensityRatio;
        double[][] obsDividedIntensityRatio;

        List<List<double>> listIdealIntensity;

        crystal[] crystals;

        private void buttonGetPeakIntensities_Click(object sender, EventArgs e)
        {
            /*
            //�f�[�^�����W�ł����ԂɂȂ��Ă��邩���`�F�b�N
            if (formMain.checkedListBoxProfiles.Items.Count < 2 || !((DiffractionProfile)formMain.checkedListBoxProfiles.Items[0]).IsLPOmain)
                return;
            if (formMain.checkedListBoxCrystals.SelectedIndex < 0 || !formMain.checkedListBoxCrystals.GetItemChecked(formMain.checkedListBoxCrystals.SelectedIndex))
                return;
            if (!formMain.toolStripButtonFittingParameter.Checked || formMain.formFitting.dataSet1.Tables[0].Rows.Count <= 0)
                return;

            //��������f�[�^���W�J�n

            Crystal cry = (Crystal)formMain.checkedListBoxCrystals.SelectedItem;

            //���x���i�[����List��p�ӂ���
            listStrHkl = new List<string>();
            listAngle = new List<double>();
            
            listIndices = new List<List<Symmetry.PlaneIndex>>();
            listTwoTheta = new List<double>();

            listIdealIntensity = new List<List<double>>();

            int n = 0;
            obsDividedIntensity = new List<List<double>>();
            for (int j = 0; j < cry.Plane.Count; j++)
                if (cry.Plane[j].IsFittingChecked)
                    obsDividedIntensity.Add(new List<double>());
            
            //���ׂĂ�checkedListBoxProfiles�̃`�F�b�N���͂���
            for (int i = 0; i < formMain.checkedListBoxProfiles.Items.Count; i++)
                formMain.checkedListBoxProfiles.SetItemChecked(i, false);

            for (int i = 1; i < formMain.checkedListBoxProfiles.Items.Count; i++)//whole�p�^�[�����͂����̂�i��1����͂��܂�
            {
                string s = ((DiffractionProfile)formMain.checkedListBoxProfiles.Items[i]).Name;
                listAngle.Add(Convert.ToDouble(s.Remove(0, s.LastIndexOf('-') + 1)));
                formMain.checkedListBoxProfiles.SetItemChecked(i, true);
                formMain.checkedListBoxProfiles.SelectedIndex = i;
                Application.DoEvents();
                n=0;
                for (int j = 0; j < cry.Plane.Count; j++)
                    if (cry.Plane[j].IsFittingChecked)
                    {
                        if (i == 1)//�ŏ��̂Ƃ���
                        {
                            listStrHkl.Add(cry.Plane[j].strHKL);
                            listTwoTheta.Add(cry.Plane[j].ThetaCalc / 180 * Math.PI);

                            string[] str = cry.Plane[j].strHKL.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                            List<Symmetry.PlaneIndex> temp1 = new List<Symmetry.PlaneIndex>();
                            listIdealIntensity.Add(new List<double>());
                            for (int k = 0; k < str.Length; k++)
                            {
                                string[] str2 = str[k].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                List<Symmetry.PlaneIndex> temp2 = new List<Symmetry.PlaneIndex>();
                                Symmetry.IsRootIndex(Convert.ToInt32(str2[0]), Convert.ToInt32(str2[1]), Convert.ToInt32(str2[2]), cry.Symmetry, ref temp2, false);
                                
                                for (int l = 0; l < temp2.Count; l++)
                                {
                                    temp1.Add(temp2[l]);
                                    listIdealIntensity[listIdealIntensity.Count - 1].Add(cry.Plane[j].eachIntensity[k] / cry.Plane[j].Multi[k]);
                                }
                            }
                            listIndices.Add(temp1);
                        }
                        
                        obsDividedIntensity[n].Add(cry.Plane[j].observedIntensity);
                        n++;
                    }
                formMain.checkedListBoxProfiles.SetItemChecked(i, false);
            }


            //��ԏ���`�F�b�N���Č��̏�Ԃɖ߂�
            formMain.checkedListBoxProfiles.SetItemChecked(0, true);
            formMain.checkedListBoxProfiles.SelectedIndex = 0;
            Application.DoEvents();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int i = 1; i < formMain.checkedListBoxProfiles.Items.Count; i++)
                dataGridView1.Columns.Add(listAngle[i - 1].ToString(), listAngle[i - 1].ToString());

            for (int i = 0; i < listStrHkl.Count; i++)
            {
                List<string> list = new List<string>();
                for (int j = 0; j < listAngle.Count; j++)
                    list.Add(!double.IsNaN(obsDividedIntensity[i][j]) && obsDividedIntensity[i][j] !=0 ? obsDividedIntensity[i][j].ToString("00.0") : "-");
                dataGridView1.Rows.Add(list.ToArray());
            }

            */
        }


        //�s�ԍ����������߂̕���
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (listStrHkl != null && e.RowIndex < listStrHkl.Count)
                e.Graphics.DrawString(listStrHkl[e.RowIndex], dataGridView1.DefaultCellStyle.Font, 
                    new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor), 
                    e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 4);
        }


        //analyze�{�^�����������Ƃ��̏���
        private void buttonAnalyze_Click(object sender, EventArgs e)
        {
            if (listAngle == null || listAngle.Count < 1 || listStrHkl == null || listStrHkl.Count < 1) return;

            //crystal�z���������
            setCrystalArray();

            //���x���z���i�[����calcDividedIntensityRatio �� obsDividedIntensityRatio�̏�����
            calcDividedIntensity = new double[obsDividedIntensity.Count][];
            calcDividedIntensityRatio = new double[obsDividedIntensity.Count][];
            obsDividedIntensityRatio = new double[obsDividedIntensity.Count][];
            for (int i = 0; i < obsDividedIntensity.Count; i++)
            {
                calcDividedIntensity[i] = new double[obsDividedIntensity[0].Count];
                calcDividedIntensityRatio[i] = new double[obsDividedIntensity[0].Count];
                obsDividedIntensityRatio[i] = new double[obsDividedIntensity[0].Count];
            }
            //obsDividedIntensityRatio�̐ݒ�
            for (int i = 0; i < obsDividedIntensityRatio.GetLength(0); i++)
            {
                double obsSum = 0;
                for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                    if (!double.IsNaN(obsDividedIntensity[i][j]))
                        obsSum += obsDividedIntensity[i][j];
                for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                    if (!double.IsNaN(obsDividedIntensity[i][j]))
                        obsDividedIntensityRatio[i][j] = obsDividedIntensity[i][j] / obsSum;
                    else
                        obsDividedIntensityRatio[i][j] = double.NaN;
            }

            //��������crystals��probability��ω������Ă����Ă��悢Residual��������Ƃ����T��

            
            //i�Ԗڂ̃����O��m�Ԗڂ̊p�x�̉�܂Ɋ�^���錋����T��
            List<List<List<int[]>>> list = new List<List<List<int[]>>>();
            for (int i = 0; i < obsDividedIntensity.Count; i++ )
            {
                list.Add(new List<List<int[]>>());
                for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                    list[i].Add(new List<int[]>());
            }
            for(int i = 0 ; i < crystals.Length ; i++)
                for(int j=0;j<crystals[i].Contribution.Count ; j++)
                    list[crystals[i].Contribution[j].i][crystals[i].Contribution[j].m].Add(new int[]{i,j});

            //�����list[i][m][?]�Ƃ����i�Ԗڂ̃����O��m�Ԗڂ̊p�x�̉�܂Ɋ�^���錋���̔ԍ��Ƃ��̌����̒��ł�Contribution�̔ԍ����킩��
            int endCount = 40;
            var tempCrystals = (crystal[])crystals.Clone();//�ύX�O��ێ�����tempCrystals
            var countCrystal = new double[crystals.Length];
            var dlg = new Crystallography.Controls.CommonDialog();
            dlg.progressBar.Maximum = endCount;
            dlg.Show();
            double residual;


            //���������z��crystalsInitial[]��ݒ肷��
            crystal[][] crystalsInitial = new crystal[obsDividedIntensity.Count][];
            for (int i = 0; i < obsDividedIntensity.Count; i++)
            {
                //�܂�i�Ԗڂ̃����O�Ɋւ��ăK�E�X�֐��I��Probability��ݒ肷��
                double[] sum = new double[obsDividedIntensity[0].Count];
                double sumTotal = 0;
                crystalsInitial[i] = (crystal[])crystals.Clone();
                for (int j = 0; j < countCrystal.Length; j++)
                {
                    countCrystal[j] = 0;
                    crystalsInitial[i][j].probability = 0;
                }
                for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                    if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                        for (int c = 0; c < list[i][m].Count; c++)
                            countCrystal[list[i][m][c][0]]++; //list[i][m][c][0]�Ԗڂ̌���������o�Ă��邩���`�F�b�N
                for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                {
                    sum[m]=0;
                    if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                        for (int c = 0; c < list[i][m].Count; c++)
                        {
                            crystalsInitial[i][list[i][m][c][0]].probability = crystals[list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio * Math.Sqrt(2) 
                                / countCrystal[list[i][m][c][0]];
                            sum[m] += crystalsInitial[i][list[i][m][c][0]].probability * crystals[list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio;
                        }
                }
                for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                    if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                        for (int c = 0; c < list[i][m].Count; c++)
                            crystalsInitial[i][list[i][m][c][0]].probability *= obsDividedIntensity[i][m] / sum[m];

                for (int n = 0; n < endCount / 4; n++)
                {
                    dlg.progressBar.Maximum = endCount/4;
                    dlg.progressBar.Value = n;
                    residual = 0;
                    for (int j = 0; j < countCrystal.Length; j++) countCrystal[j] = 0;//countCrystal��������;
                    //�܂��v�Z��̃����O�̊e�����̐ώZ�l�ƑS���̐ώZ�l�����߂�
                    sum = new double[obsDividedIntensity[i].Count];
                     sumTotal = 0;
                    for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                    {
                        sum[m]=0;
                        if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                            for (int c = 0; c < list[i][m].Count; c++)
                            {
                                sum[m] += crystalsInitial[i][list[i][m][c][0]].probability * crystalsInitial[i][list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio;
                                countCrystal[list[i][m][c][0]]++;//list[i][m][c][0]�Ԗڂ̌���������o�Ă��邩���`�F�b�N
                            }
                        sumTotal += sum[m];
                    }
                    tempCrystals = (crystal[])crystalsInitial[i].Clone();
                    for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                        if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                        {
                            double d = obsDividedIntensityRatio[i][m] / (sum[m] / sumTotal);
                            if (!double.IsNaN(d))
                            {
                                for (int c = 0; c < list[i][m].Count; c++)
                                {
                                    double a = (tempCrystals[list[i][m][c][0]].probability * (d - 1))
                                         * tempCrystals[list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio * Math.Sqrt(2)
                                        / countCrystal[list[i][m][c][0]];

                                    crystalsInitial[i][list[i][m][c][0]].probability += a;
                                    if (crystalsInitial[i][list[i][m][c][0]].probability < 0.0000001) crystalsInitial[i][list[i][m][c][0]].probability = 0.0000001;
                                }

                                residual += Math.Log(d)*Math.Log(d);
                            }
                        }
                    dlg.Text = "Calculating ....  " + residual.ToString();
                    DrawPictureBox();
                    Application.DoEvents();
                }
            }
            //���������z����������킹��
            for (int i = 0; i < crystalsInitial.Length; i++)
            {
                for (int j = 0; j < crystalsInitial[i].Length; j++)
                    crystals[j].probability += crystalsInitial[i][j].probability / crystalsInitial.Length;
                }


            for (int n = 0; n < endCount; n++)
            {
                dlg.progressBar.Maximum = endCount;
                double[] sum = new double[obsDividedIntensity[0].Count];
                double sumTotal = 0;

                residual = 0;
                dlg.progressBar.Value = n;
                for (int i = 0; i < obsDividedIntensity.Count; i++)
                {//i�Ԗڂ̃����O�ɑ΂���

                    //countCrystal��������;
                    for (int j = 0; j < countCrystal.Length; j++) countCrystal[j] = 0;
                    //�܂��v�Z��̃����O�̊e�����̐ώZ�l�ƑS���̐ώZ�l�����߂�
                    sum = new double[obsDividedIntensity[i].Count];
                     sumTotal = 0;
                    for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                    {//i�Ԗڂ̃����O��m�Ԗڂ̕���
                        sum[m]=0;
                        if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                            for (int c = 0; c < list[i][m].Count; c++)
                            {
                                sum[m] += crystals[list[i][m][c][0]].probability * crystals[list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio;
                                //list[i][m][c][0]�Ԗڂ̌���������o�Ă��邩���`�F�b�N
                                countCrystal[list[i][m][c][0]] ++;
                            }
                        sumTotal += sum[m];
                    }
                    //sum[m]��sumTotal�ł���Ċ����ɂ��A���̌v�Z�l�̊����Ɗϑ��l�̊�������v����悤��probability��ݒ肷��B
                    tempCrystals = (crystal[])crystals.Clone();
                    for (int m = 0; m < obsDividedIntensity[i].Count; m++)
                        if (!double.IsNaN(obsDividedIntensityRatio[i][m]))
                        {
                            double d = obsDividedIntensityRatio[i][m] / (sum[m] / sumTotal);
                            if (!double.IsNaN(d))
                                for (int c = 0; c < list[i][m].Count; c++)
                                {
                                    double a = (tempCrystals[list[i][m][c][0]].probability * (d - 1))
                                         * crystals[list[i][m][c][0]].Contribution[list[i][m][c][1]].ratio * Math.Sqrt(2)
                                        //����m�Ԗڂ̉�܂�list[i][m].Count�̌������琬�藧���Ă���̂ŁA�ЂƂ�����̕��ς�sum[m]/list[i][m].Count�ł���
                                        //���̒l�Ƃ̔䂪��^����\���Ƃ��񂪂���B
                                        / countCrystal[list[i][m][c][0]] ;

                                    crystals[list[i][m][c][0]].probability += a;
                                    if (crystals[list[i][m][c][0]].probability < 0.0001) crystals[list[i][m][c][0]].probability = 0.0001;
                                    //if (crystals[list[i][m][c][0]].probability > 1000) crystals[list[i][m][c][0]].probability = 1000;
                                    residual += Math.Log(d) * Math.Log(d);
                                }
                        }
                }
                //�ɂ��܂���
                tempCrystals = (crystal[])crystals.Clone();
                int r = (int)numericUpDownResolution.Value;
                for (int j = 0; j < crystals.Length; j++)
                {
                    int alpha = j / (r * r);
                    int phi = (j - alpha * r * r) / r;
                    int eta = j % r;
                    double probability = crystals[j].probability;
                    int counter = 0;
                    for (int k = alpha - 1; k <= alpha + 1; k++)
                        for (int l = phi - 1; l <= phi + 1; l++)
                            for (int m = eta - 1; m <= eta + 1; m++)
                                if (k >= 0 && k < r && l >= 0 && l < r && m >= 0 && m < r)
                                {
                                    if (!(k == 0 && l == 0 && l == 0))
                                    {
                                        probability += tempCrystals[k * r * r + l * r + m].probability * 0.2;
                                        counter++;
                                    }
                                }
                    crystals[j].probability = probability / (counter / 5.0 + 1);

                }

                dlg.Text = "Calculating ....  " + residual.ToString();
                DrawPictureBox();
                Application.DoEvents();
            }
           
            dlg.Close();
            
        }

        
        //G��ԏ�̌�������^�����ܐ��̈ʒu���v�Z����B
        private void setCrystalArray()
        {
          /*  int r = (int)numericUpDownResolution.Value;
            //indices �ɃZ�b�g����Ă��邷�ׂĂ̖ʂ̃x�N�g�����v�Z����
            Crystal cry = (Crystal)formMain.checkedListBoxCrystals.SelectedItem;
            cry.SetAxis();
            List<List<Vector3D>> vectors = new List<List<Vector3D>>();
            for (int i = 0; i < listIndices.Count; i++)
            {
                vectors.Add(new List<Vector3D>());
                for (int j = 0; j < listIndices[i].Count; j++)
                {
                    Vector3D v = cry.A_Star * listIndices[i][j].h + cry.B_Star * listIndices[i][j].k + cry.C_Star * listIndices[i][j].l;
                    v.Normarize();
                    vectors[i].Add(v);
                }
            }
            //���l�����Z�b�g
            double hwhm = (double)numericUpDownHWHM.Value / 180.0 * Math.PI;
            //crystal�z��ɁA�����O�Ɋ�^�����ܐ����Z�b�g����B
            crystals = new crystal[r * r * r];
            WaitDlg dlg = new WaitDlg();
            dlg.Show();
            dlg.progressBar.Maximum = r * r *  r;
            setSinCos();
            double coeff1 = 2 / hwhm * Math.Sqrt(Math.Log(2) / Math.PI);
            double coeff2 = -4 * Math.Log(2) / hwhm / hwhm;
            for (int alpha = 0; alpha < r; alpha++)
                for (int phi = 0; phi < r; phi++)
                {
                    dlg.progressBar.Value = alpha * r * r + phi * r;
                    Application.DoEvents();
                    for (int eta = 0; eta < r; eta++)
                    {
                        int n = alpha * r * r + phi * r + eta;
                        
                        //Matrix3D matrix = getPhiRhoZ((alpha + 0.7) / r * 2 * Math.PI, (phi + 0.3) / r * 2 * Math.PI, (double)(2 * eta + 1) / r - 1);
                        Matrix3D matrix = getEuler(alpha, phi, eta);
                        crystals[n] = new crystal();
                        crystals[n].Contribution = new List<hkl>();
                        crystals[n].probability = 0;

                        //�������炷�ׂĂ̎w���ɂ��Ĕ��˂Ɋ�^���邩�ǂ������`�F�b�N����
                        for (int i = 0; i < vectors.Count; i++)
                            for (int j = 0; j < vectors[i].Count; j++)
                            {
                                Vector3D v = new Vector3D(
                                    matrix.E11 * vectors[i][j].X + matrix.E12 * vectors[i][j].Y + matrix.E13 * vectors[i][j].Z,
                                    matrix.E21 * vectors[i][j].X + matrix.E22 * vectors[i][j].Y + matrix.E23 * vectors[i][j].Z,
                                    matrix.E31 * vectors[i][j].X + matrix.E32 * vectors[i][j].Y + matrix.E33 * vectors[i][j].Z, false);
                                double dif = Math.Abs(Math.Acos(v.Z) - (Math.PI - listTwoTheta[i] / 2));
                                if (dif < hwhm * 2)
                                {
                                    double ratio = coeff1 * Math.Exp(coeff2 * dif * dif)
                                        * listIdealIntensity[i][j] * sa[alpha];
                                    double angle = Math.Atan2(v.Y, v.X) / Math.PI * 180;

                                    //��������angle��T��
                                    double step = listAngle[1] - listAngle[0];
                                    if (angle < 0)
                                        angle += 360;
                                    int m1 = (int)(angle / step);
                                    if (m1 > listAngle.Count - 1)
                                        m1 = 0;
                                    int m2 = m1 + 1;
                                    if (m2 > listAngle.Count - 1)
                                        m2 = 0;
                                    double ratio1 = Math.Min(Math.Abs(m2 - angle / step), Math.Abs(m2 - (angle - 360) / step));
                                    double ratio2 = 1 - ratio1;

                                    if (!double.IsNaN(obsDividedIntensity[i][m1]) && obsDividedIntensity[i][m1] != 0)//���x�������Ƌ��߂��Ă���Ƃ�����
                                        crystals[n].Contribution.Add(new hkl(i, m1, ratio * ratio1));
                                    if (!double.IsNaN(obsDividedIntensity[i][m2]) && obsDividedIntensity[i][m2] != 0)//���x�������Ƌ��߂��Ă���Ƃ�����
                                        crystals[n].Contribution.Add(new hkl(i, m2, ratio * ratio2));
                                }
                            }
                        //�d�������邩�ǂ������`�F�b�N����
                        for (int i = 0; i < crystals[n].Contribution.Count; i++)
                            for (int j = i + 1; j < crystals[n].Contribution.Count; j++)
                                if (crystals[n].Contribution[i].i == crystals[n].Contribution[j].i && crystals[n].Contribution[i].m == crystals[n].Contribution[j].m)
                                {
                                    crystals[n].Contribution[i] = new hkl(crystals[n].Contribution[i].i, crystals[n].Contribution[i].m, crystals[n].Contribution[i].ratio + crystals[n].Contribution[j].ratio);
                                    crystals[n].Contribution.RemoveAt(j);
                                    j--;
                                }

                        //�Ō�ɉ�����ꂽ�����O�ԍ����v�Z����iList�Ɋi�[����
                        //List<int> listIndex = new List<int>();
                        //for (int i = 0; i < crystals[n].Contribution.Count; i++)
                        //    if (listIndex.Contains(crystals[n].Contribution[i].i))
                        //        listIndex.Add(crystals[n].Contribution[i].i);
                        //crystals[n].iList = listIndex.ToArray();
                    }
                }
            dlg.Close();
           * 
           */ 
        }
        
        
        
        //G���(phi,z,rho)�̃}�g���b�N�X���쐬���郁�\�b�h
        private Matrix3D getPhiRhoZ(double phi, double rho, double z)
        {
            Matrix3D a = new Matrix3D(Math.Cos(phi), Math.Sin(phi), 0, -Math.Sin(phi), Math.Cos(phi), 0, 0, 0, 1);
            Matrix3D b = new Matrix3D(z, 0, Math.Sqrt(1 - z * z), 0, 1, 0, -Math.Sqrt(1 - z * z), 0, z);
            Matrix3D c = new Matrix3D(Math.Cos(rho), Math.Sin(rho), 0, -Math.Sin(rho), Math.Cos(rho), 0, 0, 0, 1);
            return a * b * c;
        }
        private Matrix3D getEuler(double alpha, double phi, double eta)
        {
            double ca = Math.Cos(alpha);
            double sa = Math.Sin(alpha);
            double cp = Math.Cos(phi);
            double sp = Math.Sin(phi);
            double ce = Math.Cos(eta);
            double se = Math.Sin(eta);

            return new Matrix3D(ca * ce * cp - se * sp, ca * cp * se + ce * sp, -cp * sa, -cp * se - ca * ce * sp, ce * cp - ca * se * sp, sa * sp, ce * sa, sa * se, ca);
        }
        
        double[] ca, sa, cp, sp, ce, se;
        private void setSinCos()
        {
            int r = (int)numericUpDownResolution.Value;
            ca = new double[r]; sa = new double[r]; cp = new double[r]; sp = new double[r]; ce = new double[r]; se = new double[r];
            for (int alpha = 0; alpha < r; alpha++)
            {
                ca[alpha] = Math.Cos((double)(alpha + 0.5) / r * Math.PI);
                sa[alpha] = Math.Sin((double)(alpha + 0.5) / r * Math.PI);
            }
            for (int phi = 0; phi < r; phi++)
            {
                cp[phi] = Math.Cos((phi + 0.3) / r * 2 * Math.PI);
                sp[phi] = Math.Sin((phi + 0.3) / r * 2 * Math.PI);
            }
            for (int eta = 0; eta < r; eta++)
            {
                ce[eta] = Math.Cos((eta + 0.7) / r * 2 * Math.PI);
                se[eta] = Math.Sin((eta + 0.7) / r * 2 * Math.PI);
            }
        }

        private Matrix3D getEuler(int alpha, int phi, int eta)
        {
            return new Matrix3D(
                ca[alpha] * ce[eta] * cp[phi] - se[eta] * sp[phi],
                ca[alpha] * cp[phi] * se[eta] + ce[eta] * sp[phi],
                -cp[phi] * sa[alpha],
                -cp[phi] * se[eta] - ca[alpha] * ce[eta] * sp[phi],
                ce[eta] * cp[phi] - ca[alpha] * se[eta] * sp[phi],
                sa[alpha] * sp[phi], ce[eta] * sa[alpha],
                sa[alpha] * se[eta], 
                ca[alpha]);
        }

        private void setCalcDividedIntensity(int num, double oldValue, double newValue)
        {
            //num<0�̂Ƃ��͂��ׂĂ̌����ɑ΂��Čv�Z���Ȃ���
            if (num < 0)
            {
                for (int i = 0; i < obsDividedIntensity.Count; i++)
                    for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                        calcDividedIntensity[i][j] = 0;

                for (int i = 0; i < crystals.Length; i++)
                    for (int j = 0; j < crystals[i].Contribution.Count; j++)
                        calcDividedIntensity[crystals[i].Contribution[j].i][crystals[i].Contribution[j].m]
                            += crystals[i].Contribution[j].ratio * crystals[i].probability;
            }
            //num�Ԗڂ̌����̑��݊m����ύX�����Ƃ�
            else
            {
                for (int j = 0; j < crystals[num].Contribution.Count; j++)
                    calcDividedIntensity[crystals[num].Contribution[j].i][crystals[num].Contribution[j].m] +=
                        crystals[num].Contribution[j].ratio * (newValue - oldValue);
            }

        }

        //���݂�crystal�z��Ɗϑ��l�Ƃ́u����v�����߂郁�\�b�h
        private double getResidual(int[] num)
        {
            //num�͕ύX���ꂽ�����O�̔ԍ�
            if (num.Length == 0)
            {
                //obsIntensity, calcIntensity�̃����O�̋��x�̑��a���p�x�������Ɉ�v����悤�ɕϊ�
                for (int i = 0; i < calcDividedIntensityRatio.GetLength(0); i++)
                {
                    double calcSum = 0;
                    int n = 0;
                    for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                        if (!double.IsNaN(obsDividedIntensity[i][j]))
                        {
                            calcSum += calcDividedIntensity[i][j];
                            n++;
                        }
                    for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                        if (!double.IsNaN(obsDividedIntensity[i][j]))
                            calcDividedIntensityRatio[i][j] = calcDividedIntensity[i][j] / calcSum * n;
                }
            }
            else
            {
                for (int i = 0; i < num.Length; i++)
                {
                    double calcSum = 0;
                    int n = 0;
                    for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                        if (!double.IsNaN(obsDividedIntensity[num[i]][j]))
                        {
                            calcSum += calcDividedIntensity[num[i]][j];
                            n++;
                        }
                    for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                        if (!double.IsNaN(obsDividedIntensity[num[i]][j]))
                            calcDividedIntensityRatio[num[i]][j] = calcDividedIntensity[num[i]][j] / calcSum * n;
                }
            }


            //�Ō�Ɏc���̓��a���Ƃ��ĕԂ�
            double residual = 0;
            for (int i = 0; i < obsDividedIntensity.Count; i++)
                for (int j = 0; j < obsDividedIntensity[0].Count; j++)
                    if (!double.IsNaN(obsDividedIntensity[i][j]))
                        residual += (calcDividedIntensityRatio[i][j] - obsDividedIntensityRatio[i][j]) * (calcDividedIntensityRatio[i][j] - obsDividedIntensityRatio[i][j]);
            return residual;
        }

        private void DrawPictureBox()
        {
         /*   if (crystals == null || crystals.Length < 1) return;
            //a���̕��z��T��
            Crystal cry = (Crystal)formMain.checkedListBoxCrystals.SelectedItem;
            cry.SetAxis();
            Vector3D a = cry.A_Axis;
            a.Normarize();
            Vector3D b = cry.B_Axis;
            b.Normarize();
            Vector3D c = cry.C_Axis;
            c.Normarize();
            DrawDensity(a, pictureBoxA);
            DrawDensity(b, pictureBoxB);
            DrawDensity(c, pictureBoxC);
           */ 
        }

        private void DrawDensity(Vector3D v, PictureBox picturebox)
        {
            //�܂��s�N�`���[�{�b�N�X�̑傫���ɉ����Ĕz������

            int pixelSize = 3;
            int length = (picturebox.Width - 10) / pixelSize ;
            //int length = 36 ;


            double[][] density = new double[length][];
            for (int i = 0; i < length; i++)
            {
                density[i] = new double[length];
                for (int j = 0; j < length; j++)
                    density[i][j] = 0;
            }
            //�������ǂ̃s�N�Z���ɑΉ����邩�����߂Ă��̃s�N�Z����probability�𑫂�
            int r = (int)numericUpDownResolution.Value;
            for (int alpha = 0; alpha < r; alpha++)
                for (int phi = 0; phi < r; phi++)
                    for (int eta = 0; eta < r; eta++)
                    {
                        int n = alpha * r * r + phi * r + eta;
                        //double Alpha = (double)(alpha + 0.5) / r * Math.PI;
                        //double Phi = (double)phi / r * 2 * Math.PI + Math.PI / r;
                        //double Eta = (double)eta / r * 2 * Math.PI;
                        //double Alpha = 1;
                        //Matrix3D matrix = getPhiRhoZ((alpha + 0.7) / r * 2 * Math.PI, (phi + 0.3) / r * 2 * Math.PI, (double)(2 * eta + 1) / r - 1);
                        //Matrix3D matrix = getEuler(Alpha, Phi, Eta);
                        Vector3D vector = getEuler(alpha, phi, eta) * v;
                        //���̃x�N�g��(X,Y,Z)���V���~�b�g�l�b�g�Ƀv���b�g�����Ƃ���(x,y) = ( X/��(2-2Z),Y/��(2-2Z) )
                        double x, y;
                        x = vector.X / Math.Sqrt(2 - 2 * vector.Z);
                        y = vector.Y / Math.Sqrt(2 - 2 * vector.Z);

                        //x,y�̂����ɉ����ċ��x������U��

                        double X = (x + 1) / 2 * (length-1);
                        double Y = (y + 1) / 2 * (length-1);
                        int x1 = (int)X;
                        int x2 = x1 + 1;
                        int y1 = (int)Y;
                        int y2 = y1 + 1;

                        if (x1 < 0) x1 = 0;
                        if (x2 < 0) x2 = 0;
                        if (y1 < 0) y1 = 0;
                        if (y2 < 0) y2 = 0;
                        if (x1 > length - 1) x1 = length - 1;
                        if (x2 > length - 1) x2 = length - 1;
                        if (y1 > length - 1) y1 = length - 1;
                        if (y2 > length - 1) y2 = length - 1;

                        //if (Alpha != 0)
                        {
                            density[x1][y1] += crystals[n].probability * (1 - X + (int)X) * (1 - Y + (int)Y) * sa[alpha];
                            density[x1][y2] += crystals[n].probability * (1 - X + (int)X) * (Y - (int)Y) * sa[alpha];
                            density[x2][y1] += crystals[n].probability * (X - (int)X) * (1 - Y + (int)Y) * sa[alpha];
                            density[x2][y2] += crystals[n].probability * (X - (int)X) * (Y - (int)Y) * sa[alpha];
                      
                            
                            //density[I][J] += crystals[n].probability * Math.Sin(Alpha);
                        }
                    }

            //�ɂ��܂���
            double[][] density2 = new double[length][];
            for (int i = 0; i < length; i++)
            {
                density2[i] = new double[length];
                for (int j = 0; j < length; j++)
                    density2[i][j] = 0;
            }
            int h =1;
            if (h == 0)
            {
                for (int i = 0; i < length; i++)
                    for (int j = h; j < length; j++)
                        density2[i][j] += density[i][j];
            }
            else
            {
                for (int i = 2 * h; i < length - 2 * h - 1; i++)
                    for (int j = 2 * h; j < length - 2 * h - 1; j++)
                        for (int k = -2 * h; k <= 2 * h; k++)
                            for (int l = -2 * h; l <= 2 * h; l++)
                                density2[i][j] += 2.0 / h * Math.Sqrt(Math.Log(2) / Math.PI) * Math.Exp(-4 * Math.Log(2) * (k * k + l * l) / h / h) * density[i + k][j + l];
            }

            //�k�����ɂ���_���폜����
            for (int i = 0; i < length; i++)
                for (int j = h; j < length; j++)
                {
                    double x = 2.0 * i  / (length-1) - 1;
                    double y = 2.0 * j  / (length-1) - 1;
                    //if (x * x + y * y > 1 / Math.Sqrt(2))
                    //    density2[i][j] = 0;
                }

             double max = double.NegativeInfinity;
             for (int i = 0; i < length; i++)
                 for (int j = 0; j < length; j++)
                     if (max < density2[i][j])
                         max = density2[i][j];

             Bitmap bmp = new Bitmap(picturebox.Width, picturebox.Height);

             double sum = 0;
             for (int i = 0; i < length; i++)
                 for (int j = 0; j < length; j++)
                 {
                     sum += density[i][j];
                     int a = (int)(255 - density2[i][j] / max * 255);
                     if (a < 0) a = 0;
                     if (a > 255) a = 255;
                     for (int k = 0; k < pixelSize; k++)
                         for (int l = 0; l < pixelSize; l++)
                            bmp.SetPixel(5 + pixelSize * i + k, 5 + pixelSize * j + l, Color.FromArgb(a, a, a));
                 }
             picturebox.Image = bmp;
        }

        private void DrawOutLine()
        {

            
        }
        

        struct crystal
        {
            public double probability;
            public List<hkl> Contribution;
            public override string ToString()
            {
                return Contribution.Count.ToString() +"  " + probability.ToString();
            }
            
        }
        struct hkl
        {
            public int i,m;//i�Ԗڂ̃����O�́Am�Ԗڂ̉�܊p�x
            public double ratio;//��܂����Ƃ��̋��x�̔�
            public hkl(int i, int m, double ratio)
            {
                this.i = i;
                this.m = m;
                this.ratio = ratio;
            }
            public override string ToString() {
                return "i: " + i.ToString()+"   m: "+m.ToString() + "   ratio: "+ratio.ToString();
            }

        }




    }
}