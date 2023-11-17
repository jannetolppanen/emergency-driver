<?php
ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);
class HighScores {
    const SERVERNAME = "niisku.lab.fi:3306";
    const USERNAME = "";
    const PASS = "";
    const DBNAME = "";
    
    private $conn = null;
    
    function __construct() {
        ;
    }
    
    function initConnection() {
        $this->conn = new mysqli(self::SERVERNAME, self::USERNAME, self::PASS, self::DBNAME);
        
        //CHECK CONNECTION
        
        if( $this->conn->connect_error) {
            die("Connection failed: ".$this->conn->connect_error);
        }
    }
    
    function queryhighScores() {
        if($this->conn) {
            $reply = array("scores" => array());
            
            $sql = "SELECT * FROM HIGHSCORES ORDER BY score ASC LIMIT 3";

            if(($result=$this->conn->query($sql))) {
                while(($row=$result->fetch_assoc())) {
                    $reply["scores"][] = $row;
                }
                $result->close();
                return $reply;
            }
        } else {
            $reply = array();
            $reply['status'] = "Error";
            $reply['msg'] = "DB connection not open";
            return $reply;
        }
    }
    function insertHighScore($playername, $score) {
        if($this->conn){
            $pn = filter_var($playername, FILTER_SANITIZE_FULL_SPECIAL_CHARS);

            $sc = filter_var($score, FILTER_VALIDATE_FLOAT);
        
            if($pn and $sc) {
                $sql = 'INSERT INTO HIGHSCORES (playername, score) VALUES ("'.$pn.'",'.$sc.')';
                if ($this->conn->query($sql)=== TRUE) {
                    return 'OK';
                    
                } else {
                    return $this->conn->error;
                }
            } else {
                return 'parameters can not be empty';
            }
            
        } else {
            return 'DB connection error';
        }
    }
    function closeConnection() {
        if($this->conn) {
            $this->conn->close();
        }
    }
}

$hs = new HighScores();
$hs->initConnection();

header('Content-Type: application/json');

if($_SERVER['REQUEST_METHOD'] === 'GET') {
    $response = $hs->queryhighScores();
    
    echo json_encode($response);
}
if($_SERVER['REQUEST_METHOD'] === 'POST') {
    $data = file_get_contents('php://input');
     error_log("Received data from Unity: " . $data); // Add this line for debugging
     
    $data_decoded = urldecode($data);
    
    $hsItem = json_decode($data_decoded, true);
    
    $ret = $hs->insertHighScore ($hsItem['playername'], $hsItem['score']);
    
    $response["status"] = $ret;
    $response["dbg"] = "POST received".$hsItem['playername'].': '.$hsItem['score'];
    
    echo json_encode($response);
}
$hs->closeConnection();


