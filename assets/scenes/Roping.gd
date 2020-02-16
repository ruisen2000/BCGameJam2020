extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.

var p1
var p2
var rope

func _ready():
	p1 = get_node("Player 1") # Replace with function body.
	p2 = get_node("Player 2")
	rope = get_node("rope__")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	rope.clear_points()
	rope.add_point(p1.position)
	rope.add_point(p2.position)
		
	
