<?php
	require ('inc_conn.php');
	
	if(isset($_POST["usernameWin"])){
		// Dodati varijable koje bi spremili
		$usernameWin = mysqli_real_escape_string($conn, $_POST["usernameWin"]);
		$usernameLose = mysqli_real_escape_string($conn, $_POST["usernameLose"]);

		// Mogao bi se koristiti switch za razlicita spremanja podataka
		$stmt = $conn->prepare("INSERT INTO bitka(username1, username2, winner) VALUES (?,?,0)");
		$stmt->bind_param("ss", $usernameWin, $usernameLose);
		$stmt->execute() or die("4: Insert player query failed");
		
		$stmt2 = $conn->prepare("UPDATE `users` SET `no_victory` = `no_victory` + 1 WHERE username like ?;");
		$stmt2->bind_param("s", $usernameWin);
		$stmt2->execute() or die("10: Insert player win statistics");

		$stmt1 = $conn->prepare("UPDATE `users` SET `no_lose` = `no_lose` + 1 WHERE username like ?;");
		$stmt1->bind_param("s", $usernameLose);
		$stmt1->execute() or die("10: Insert player lose statistics");	

		$stmt3 = $conn->prepare("UPDATE `users` SET `gold` = `gold` + 200, `rum` = `rum` + 150, `wood` = `wood` + 140, `experience` = `experience` + 10  WHERE `username` LIKE ?;");
		$stmt3->bind_param("s", $usernameWin);
		$stmt3->execute() or die("11: Update player win resource");
		
		
		$stmt1->close();
		$stmt2->close();
		$stmt3->close();
		$stmt->close();
		
		echo "0";
		
	}else if(isset($_POST["Strength"])){
		// Dodati varijable koje bi spremili
		$shipName = mysqli_real_escape_string($conn, $_POST["nameOfShip"]);
        $username = mysqli_real_escape_string($conn, $_POST["username"]);
        $Strength = $_POST["Strength"];
        $Cannons = $_POST["Cannons_Power"];
        $Speed = $_POST["Speed"];
        $Turn = $_POST["Turn_Speed"];
        $gold = $_POST["gold"];
        $rum = $_POST["rum"];
        $wood = $_POST["wood"];
        $pearl = $_POST["pearl"];
		
		// Mogao bi se koristiti switch za razlicita spremanja podataka
		$stmt = $conn->prepare("UPDATE users, user_ship SET users.gold = ?, users.rum = ?, users.wood = ?, users.pearl = ?, user_ship.strength = ?, user_ship.singleCannonDmg = ?, user_ship.speed = ?, user_ship.turn_speed = ? WHERE users.username like ? AND users.username like user_ship.username AND user_ship.shipName like ?;");
		$stmt->bind_param("iiiiiiiiss", $gold, $rum, $wood, $pearl, $Strength, $Cannons, $Speed, $Turn, $username, $shipName);
		$stmt->execute() or die("4: Update ship and user resources query failed.");
		$stmt->close();
		echo "0";
		
	}else if(isset($_POST["nameOfShip"])){
		
		// Dodati varijable koje bi spremili
		$shipName = mysqli_real_escape_string($conn, $_POST["nameOfShip"]);
		$username = mysqli_real_escape_string($conn, $_POST["username"]);
		$gold = mysqli_real_escape_string($conn, $_POST["gold"]);
        $rum = mysqli_real_escape_string($conn, $_POST["rum"]);
        $wood = mysqli_real_escape_string($conn, $_POST["wood"]);
        $pearl = mysqli_real_escape_string($conn, $_POST["pearl"]);
		
		// Mogao bi se koristiti switch za razlicita spremanja podataka
		$stmt = $conn->prepare("INSERT INTO `user_ship`(`username`, `shipName`) VALUES (?,?);");
		$stmt->bind_param("ss", $username, $shipName);
		$stmt->execute() or die("4: Insert ship query failed");

		$stmt = $conn->prepare("UPDATE users SET users.gold = ?, users.rum = ?, users.wood = ?, users.pearl = ? WHERE users.username like ?;");
		$stmt->bind_param("iiiis", $gold, $rum, $wood, $pearl, $username);
		$stmt->execute() or die("9: Update user resources failed during the purchase ship.");

		$stmt->close();
		echo "0";
	}
?>