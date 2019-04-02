<?php
	$conn = mysqli_connect("localhost", "root", "", "thewarships");

	if (!$conn) {
		echo "1: Connection failed";
		exit;
	}
?>