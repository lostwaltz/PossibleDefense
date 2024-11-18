   using UnityEngine;
    public class TowerController : MonoBehaviour 
    {
        //선택된 타워를 저장
        private GameObject selectedTower;
        //Mouse 좌표를 받아서 수행  ,Ray 사용 
        
        //Mouse OnClick  
        
        
        // OnClick - 1초미만 info UI  , 1초 이상 movement  
        
        
        //Case 1: info UI  info UI를 보여줌 

        //tower 소환을 누가 할지 ?   - TowerSpawner를 통해 소환! 
        
        //tile 정보를 받아올 수 있는 부분이 필요함. 해당 위치에 tower가 배치 돼있는가?  -> StageManager로 부터 받아옴 정보를 ?
        
        
        // tile 정보 관리 주체 - stageManager  // 이걸 확인하기 위한 리스트? 
        
        
        //서로 데이터를 교환할 수 있어야 towerController에서 Event = > stageManager에서 받아서 처리   
        
        
        //Case 2: Movement 플레이어가 선택한 타일로 움직일 수 있도록 해줌 
        //배치가 없다면 이동 가능 배치 돼있다면 이동 안됨 
        // 움직이는게 확정 되기 전까지는 stateMachine 건들지 않음! 


        
        //빈 곳 클릭 시
        // selectedTower null처리
        
        
        //UpdateUI selectedTower와 관련된 UI 보여줌. 
     
        
        //ObjectPool Get 느낌맨키로 빈 타일 정보 받아서 spawn 
        //이동경우일 경우에는 내가 누른 타일이 빈 타일인지를 알 수 있어야함! 
        
        
        
        // 빈 타일인지 아닌지 - StageManager  
        // 시네머신 카메라 시점을  천장에서 내려다보는 시점으로 변경해서 
        // 타일 클릭하면 그 곳으로 이동 
        
    }