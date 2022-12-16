using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boss_Patterndata 
{

    public enum Sequence
    {
        Phase_First_01,    // ������ 1�� ����01
        Phase_First_02,    // ������ 1�� ����02
        Phase_Second,      // �ι�° ������
        Final_Phase,       // ���̳� ������

    }

    public Sequence key;

    // Inpectorâ���� ������ �� ������ ���� BossController�� enum ����
    public List<BossController.eState> act;
   
    public bool doNext = true;
    public int coolTime;

}
