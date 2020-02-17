// ****************************************************************
// Unity Game Engine Example
// Everything in this file by MrLarodos: http://www.youtube.com/user/MrLarodos
//
// Released under the Creative Commons Attribution 3.0 Unported License:
// http://creativecommons.org/licenses/by/3.0/de/
// http://creativecommons.org/licenses/by/3.0/
//
// If you use this file or parts of it, you have to include this information header.
//
// DEUTSCH:
// Wenn Du diese Datei oder Teile davon benutzt, musst Du diesen Infoteil beilegen.
// ****************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai : MonoBehaviour {

	bool debug;

	bool player_in_range;
	bool player_in_sight;
	bool player_in_touch;
	bool ob_in_sight;
	bool idle;
	bool walking;
	bool turning;
	bool attacking;

	bool HitDetect;
	bool first_turn;

	float timer;
	float min_time;
	float cur_speed;
	float nor_speed;
	float add_hunt_speed;
	float smooth;
	float hit_sight_range;
	float sight_player_range;
	float player_notice_range;
	float hit_sight_box_size;

	Quaternion target;

	int target_look;
	float wait_between_action_min;
	int wait_between_action_max;

	int chance_get_idle;
	int chance_leave_idle;
	int chance_do_turn;
	int chance_keep_direction;

	RaycastHit hit;
	RaycastHit see;
	Collider own_collider;
	Vector3 sight_box;

	string last_info;
	string player_name;

	GameObject player;

	void Awake() {
		
		//#####################################################
		debug=false;

		chance_get_idle=5; //Prozentuale Chance, dass der Gegner idle / untätig wird
		chance_leave_idle=10; //Prozentuale Chance, dass der Gegner aus der Untätigkeit erwacht
		chance_do_turn=10; //Prozentuale Chance, dass der Gegner die Richtung ändert
		chance_keep_direction=85; //Prozentuale Chance, dass der Gegner die Richtung beibehält

		wait_between_action_min=1; //Wie lange bleibt der Gegner minimal im aktuellen Status, zum Beispiel idle
		wait_between_action_max=3; //Wie lange bleibt der Gegner maximal im aktuellen Status, zum Beispiel idle

		smooth=4.0F; //Drehung weicher
		nor_speed=1.0F; //Normale Geschwindikeit
		add_hunt_speed=0.5F; //Zusätliche Geschwindikeit (nor_speed + add_hunt_speed) beim Verfolgen

		hit_sight_box_size=0.45F; //Größe der Sicht-Hitbox - nicht so groß machen, dass die im Boden steckt!
		hit_sight_range=1.0F; //Entfernung der Sicht-Hitbox

		player_notice_range=6.0F; //Ab welcher Distanz wird der Spieler bei Annäherung (auch von hinten) bemerkt?
		sight_player_range=player_notice_range+2.0F; //In welcher Distanz wird der Spieler gesehen?
		//#####################################################

		first_turn=true;
		player_in_range=false;
		player_in_sight=false;
		player_in_touch=false;
		ob_in_sight=false;
		idle=false;
		walking=false;
		turning=false;
		attacking=false;
		timer=0F;
		min_time=0F;
		target_look=0;

		last_info="";

		own_collider = GetComponent<Collider>();
		sight_box = new Vector3(hit_sight_box_size, hit_sight_box_size, hit_sight_box_size);

	}

	void OnDrawGizmos(){
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, transform.forward * see.distance);

		if ( HitDetect ){
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
			Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, new Vector3(hit_sight_box_size*2, hit_sight_box_size*2, hit_sight_box_size*2) );
		}else{
			Gizmos.color = Color.green;
			Gizmos.DrawRay(transform.position, transform.forward * hit_sight_range);
			Gizmos.DrawWireCube(transform.position + transform.forward * hit_sight_range, new Vector3(hit_sight_box_size*2, hit_sight_box_size*2, hit_sight_box_size*2) );
		}

	}

	void FixedUpdate(){

		//START Hindernisse sehen#######################################################################################
		HitDetect = Physics.BoxCast(own_collider.bounds.center, sight_box, transform.forward, out hit, transform.rotation, hit_sight_range);
		player_name = "";

		if( HitDetect ){
			if( hit.transform.tag=="wall" ){
				ob_in_sight=true;
				player_in_touch=false;
			}else if( hit.transform.tag=="Player" ){
				player_in_touch=true;
				player_name = hit.transform.name;
			}else if( hit.transform.tag=="door" ){
			}else{
				ob_in_sight=true;
				player_in_touch=false;
			}
		}else{
			ob_in_sight=false;
			player_in_touch=false;
		}
		//ENDE Hindernisse sehen#######################################################################################

		//START Spieler bemerken/sehen#######################################################################################
		if( Physics.Raycast(transform.position, transform.forward, out see, sight_player_range) ) {
			if( see.transform.tag=="Player" ){
				player_in_sight=true;
				cur_speed=nor_speed+add_hunt_speed;
			}else{
				player_in_sight=false;
				cur_speed=nor_speed;
			}
		}else{
			cur_speed=nor_speed;
			player_in_sight=false;
		}

		player = GameObject.FindGameObjectWithTag("Player");
		if(player){

			float dist = Vector3.Distance(player.transform.position, transform.position);
			
			if( dist <= player_notice_range ){
				player_in_range=true;
			}else if( dist >= (player_notice_range + (player_notice_range * 0.15F)) ){
				player_in_range=false;
			}

		}
		//ENDE Spieler bemerken/sehen#######################################################################################

		//START Idle#######################################################################################
		// print("chance:"+chance_get_idle+"|timer:"+timer+"|min_time:"+min_time+"|idle:"+idle+"|turning:"+turning);
		if( idle==false && turning==false && player_in_range==false && player_in_sight==false && (int)Random.Range(0,101)<=chance_get_idle && timer >= min_time ){
			// print("Idle on");
			idle=true;
			timer=0F;
			min_time=(float)Random.Range(wait_between_action_min,wait_between_action_max);

		}else if( idle==true && (int)Random.Range(0,101)<=chance_leave_idle && timer >= min_time && turning==false ){
			// print("Idle off");
			idle=false;
			timer=0F;
			min_time=(float)Random.Range(wait_between_action_min,wait_between_action_max);
		}
		//ENDE Idle#######################################################################################

		//START Attacke#######################################################################################
		if( player_in_touch && attacking==false ){

			//Hier kommt das rein, was beim Angreifen passieren soll

		}else if( player_in_touch==false ){

			//Hier kommt das rein, was nach dem Angreifen passieren soll
			
		}
		//ENDE Attacke#######################################################################################

		//START Laufen#######################################################################################
		// if( idle==false && ob_in_sight==false && walking==false && turning==false ){
		if( idle==false && (ob_in_sight==false && walking==false && player_in_range==false || ob_in_sight==false && player_in_touch==false && walking==false && player_in_sight==true)){
			walking=true;
		}else if( idle==true && walking==true || ob_in_sight==true && walking==true || player_in_range==true && player_in_sight==false || player_in_range==true && player_in_sight==true && player_in_touch==true ){
			walking=false;
		}

		if( walking==true ){
			var moveAmount = cur_speed * Time.deltaTime;
			transform.localPosition += transform.forward * moveAmount;
		}
		//ENDE Laufen#######################################################################################

		//START Drehungen#######################################################################################
		if( idle==false && turning==false && (int)Random.Range(0,101)<=chance_do_turn && player_in_range==false ){

			turning=true;

			if( first_turn==true || first_turn==false && (int)Random.Range(0,101)>=chance_keep_direction){//Chance, dass nochmal in selbe Richtung gedreht wird
			
				target_look = (int)Random.Range(45,91);
				if( (int)Random.Range(1,3) == 1 )target_look*=-1;

				target_look = (int)transform.eulerAngles.y + target_look;
				target = Quaternion.Euler(0, target_look, 0);

			}
			
			if(first_turn==true)first_turn=false;

		}else if( turning==true && player_in_range==false ){

			double cur_y = System.Math.Round(transform.rotation.y,1);
			double tar_y = System.Math.Round(target.y,1);

			if( cur_y<0 )cur_y*=-1;
			if( tar_y<0 )tar_y*=-1;


			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

			if( cur_y==tar_y ){
				turning=false;

			}

		}else if( player_in_range==true ){
			Quaternion targetRotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smooth);
		}
		//ENDE Drehungen#######################################################################################

		timer+=Time.deltaTime;

	}
}