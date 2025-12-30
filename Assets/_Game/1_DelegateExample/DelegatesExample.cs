using UnityEngine;


public class DelegatesExample : MonoBehaviour
{
    private void Awake()
    {
        //TestDelegate testDelegate = new TestDelegate(Sum);

        //int result = testDelegate(5, 3);

        //Debug.Log(result);

        //testDelegate += Multipy;
        //testDelegate -= Sum;
        //testDelegate -= Multipy;

        //if (testDelegate != null)
        //{
        //    result = testDelegate.Invoke(5, 4);

        //    Debug.Log(result);
        //}
        ShowOperationResult(Sum, 6, 5);

        ShowOperationResult(Multipy, 6, 2);
    }

    private int Sum(int a, int b) => a + b;

    private int Multipy(int a, int b) => a * b;

    private int Substract(int a, int b) => a - b;

    private void ShowOperationResult(TestDelegate operation, int firstNumber, int secondNumber)
    {
        int result = operation.Invoke(firstNumber, secondNumber);

        Debug.Log($"{result}");
    }
}
