using UnityEngine;
using System;
using System.Collections;


public class DragGameObject : MonoBehaviour {

	public enum BoneMask
	{
		None = 0x0,
		Hip_Center = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hip_Left = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		Hip_Right = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xFFFFF,
		Torso = 0x10000F, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000F0,
		Right_Arm = 0x100F00,
		Left_Leg = 0x10F000,
		Right_Leg = 0x1F0000,
		R_Arm_Chest = Right_Arm | Spine,
		No_Feet = All & ~(Foot_Left | Foot_Right),
		UpperBody = Shoulder_Center | Head|Shoulder_Left | Elbow_Left | Wrist_Left | Hand_Left|
		Shoulder_Right | Elbow_Right | Wrist_Right | Hand_Right
		
	}
	
	public SkeletonWrapper sw;
	
	public GameObject GO_Mover;
	public int player;
	public BoneMask Mask = BoneMask.All;
	public float scaleX;
	public float scaleY;
	public Vector3 offset;


	public enum estadosMI
	{
		None=0,
		ArribaCabeza=1,
		HombroDerecho=2
	}
	private estadosMI EstIzquierdo=estadosMI.None;
	private estadosMI EstAnterior = estadosMI.None;
	private int ret=0;
	public int numRet;
	public float errorHM;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if(player == -1)
			return;
		//update all of the bones positions

		if (sw.pollSkeleton()) // si encuentra un player, asocia un Game object con la Mano derecha (Cursor del juego.)
		{
			Vector3 mDerecha = new Vector3(sw.bonePos[player,11].x,sw.bonePos[player,11].y,0.0f);
			Vector3 mIzquierda = new Vector3(sw.bonePos[player,7].x,sw.bonePos[player,7].y,sw.bonePos[player,7].z);
			Vector3 cabeza= new Vector3(sw.bonePos[player,4].x,sw.bonePos[player,4].y,sw.bonePos[player,4].z);
			Vector3 hCentro = new Vector3(sw.bonePos[player,2].x,sw.bonePos[player,2].y,sw.bonePos[player,2].z);
			Vector3 cCentro = new Vector3(sw.bonePos[player,0].x,sw.bonePos[player,0].y,sw.bonePos[player,0].z);


			Vector3 hombrod = new Vector3(sw.bonePos[player,8].x,sw.bonePos[player,8].y,sw.bonePos[player,8].z);
			

			// referenciando el movimiento horizontal de la manoDerecha al del centro de los hombros.
			Vector3 PosicionCursor = new Vector3( 
										(mDerecha.x - hombrod.x + offset.x)*-scaleX,
										(mDerecha.y - hCentro.y + offset.y)*scaleY, // referenciando el movimiento vertical de la mano al medio de la cintura y del centro de los hombros.
			                                     0.0f);
			GO_Mover.transform.position = PosicionCursor;
			// referenciando el movimiento izquierda de la manoDerecha al del centro de los hombros.
			//Vector3 PosIzquierda = new Vector3(  (mIzquierda.x - hCentro.x + offset.x)*scaleX,
			//                                     (mIzquierda.y - cCentro.y + offset.y)*scaleY, // referenciando el movimiento vertical de la mano al medio de la cintura y del centro de los hombros.
			//                                     0.0f);

			switch(EstIzquierdo)
			{
			case estadosMI.None:
				Debug.Log ("Entrando a None");
				if (EstAnterior == EstIzquierdo) // viene del mismo estado, asi que esta recargando la transicion
				{
					Debug.Log ("EstadosIguales");
					// para ir al estado ArribaCabeza:
					if(mIzquierda.y - cabeza.y > errorHM*0.1f)
					{
						ret++;
						Debug.Log ("ContandoRet");
						if(ret>numRet)
						{
							// accion en el estado AbrribaCabeza
							Debug.Log("Yendo al estado ArribaCabeza");
							EstIzquierdo = estadosMI.ArribaCabeza;// se va al estado arriba de la cabeza;
							ret = 0;
						}
					}
					else{
						//Debug.Log(Math.Abs(mIzquierda.x*1.0f - hombrod.x*1.0f));
						//Debug.Log(Math.Abs(mIzquierda.y*1.0f - hombrod.y*1.0f);
					// para ir al estado HombroDerecho:
						if(Math.Abs(mIzquierda.x*1.0f - hombrod.x*1.0f)<errorHM && Math.Abs(mIzquierda.y*1.0f - hombrod.y*1.0f)<errorHM)
						{
							ret++;
							Debug.Log ("mano cerca de hombro");
							if(ret>numRet)
							{
								// accion en el estado Hombro izquierdo.
								Debug.Log("Yendo al estado Hombro Derecho");
								EstIzquierdo = estadosMI.HombroDerecho; // se va al estado Hombro Derecho.
								ret = 0;
							}
						}
						else
						{
							EstIzquierdo = estadosMI.None;
							Debug.Log ("No se cumple ninguna");
						}
					}
				}
				else // se ejecuta si no coninciden los estados actual, y anterior. Por lo que ret debe reiniciarse, y nodificar la igualdad de los estados.
				{
					Debug.Log ("Diferentes");
					ret=0;
					EstAnterior = EstIzquierdo;
				}
				break;
			case estadosMI.ArribaCabeza:
				if(EstAnterior == EstIzquierdo)
				{
					// permanece en el estado Arriba Cabeza:
					if(mIzquierda.y - cabeza.y > errorHM*0.1f)
					{
						EstIzquierdo = estadosMI.ArribaCabeza;
						ret=0;
					}
					else // trata de volver al estado None:
					{
						ret++;
						if(ret>numRet)
						{
							// accion en el estado None
							Debug.Log("Yendo al estado None");
							EstIzquierdo = estadosMI.None;// se va al estado None;
							ret = 0;
						}
					}
						
				}
				else
				{
					EstAnterior = EstIzquierdo;
					ret=0;
				}
				break;
			case estadosMI.HombroDerecho:

				if(EstAnterior == EstIzquierdo)
				{

					// si sigue cumpliendo la condicion del estado hombro, continua en el estado hombro.
					if(Math.Abs(mIzquierda.x*1.0f - hombrod.x*1.0f)<errorHM && Math.Abs(mIzquierda.y*1.0f - hombrod.y*1.0f)<errorHM)
					{
						EstIzquierdo = estadosMI.HombroDerecho;
					}
					else{
						ret++;
						if(ret>numRet)
						{
							// accion en el estado None
							Debug.Log("Yendo al estado None");
							EstIzquierdo = estadosMI.None;// se va al estado None;
							ret = 0;
						}
					}
				}
				else
				{
					EstAnterior = EstIzquierdo;
					ret=0;
				}

				break;
			default:
				Debug.Log("Estado default");
				break;
			}

		}
	}
}
