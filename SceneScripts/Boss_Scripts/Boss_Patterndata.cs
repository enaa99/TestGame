using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boss_Patterndata 
{

    public enum Sequence
    {
        Phase_First_01,    // 페이즈 1의 패턴01
        Phase_First_02,    // 페이즈 1의 패턴02
        Phase_Second,      // 두번째 페이즈
        Final_Phase,       // 파이널 페이즈

    }

    public Sequence key;

    // Inpector창에서 지정할 때 편리성을 위해 BossController의 enum 설정
    public List<BossController.eState> act;
   
    public bool doNext = true;
    public int coolTime;

}
