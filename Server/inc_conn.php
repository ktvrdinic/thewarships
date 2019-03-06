<?php
	$conn = mysqli_connect("localhost", "root", "", "theWarships");

	if (!$conn) {
		echo "1: Connection failed";
		exit;
	}
?>