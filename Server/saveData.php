<?php
	require ('inc_conn.php');
    
	
	
	if(isset($_POST["winner"])){
		
		// Dodati varijable koje bi spremili
		$username = mysqli_real_escape_string($conn, $_POST["username"]);
		$username_enemy = mysqli_real_escape_string($conn, $_POST["username_enemy"]);
		$winner = mysqli_real_escape_string($conn, $_POST["winner"]);
		// Mogao bi se koristiti switch za razlicita spremanja podataka
		$stmt = $conn->prepare("INSERT INTO bitka(username1, username2, winner) VALUES (?,?,?)");
		$stmt->bind_param("ssi", $username, $username_enemy, $winner);
		$stmt->execute() or die("4: Insert player query failed");
		$stmt->close();
		echo "0";
	}else if(isset($_POST["nameOfShip"])){
		
		// Dodati varijable koje bi spremili
		$shipName = mysqli_real_escape_string($conn, $_POST["nameOfShip"]);
		$username = mysqli_real_escape_string($conn, $_POST["username"]);
		
		// Mogao bi se koristiti switch za razlicita spremanja podataka
		$stmt = $conn->prepare("INSERT INTO `user_ship`(`username`, `shipName`) VALUES (?,?);");
		$stmt->bind_param("ss", $username, $shipName);
		$stmt->execute() or die("4: Insert ship query failed");
		$stmt->close();
		echo "0";
	}    
?>