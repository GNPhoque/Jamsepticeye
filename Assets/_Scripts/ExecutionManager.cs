using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExecutionManager : MonoBehaviour
{
	[SerializeField]
	public SaveData saveData;
	[SerializeField]
	private List<MachineSO> machines;

	public MachineSO machine;
	private void Start()
	{
		machine = machines.First(x => x.machine == saveData.selectedMachine);
	}
}
