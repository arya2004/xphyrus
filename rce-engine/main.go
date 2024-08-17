package main

import (
	"fmt"
	"net/http"
	"os"

	"github.com/Xphurus/rce-engine/handler"
	"github.com/runabol/tork/cli"
	"github.com/runabol/tork/conf"
	"github.com/runabol/tork/engine"
)

func main() {
	if err := conf.LoadConfig(); err != nil {
		fmt.Println(err)
		os.Exit(1)
	}

	engine.RegisterEndpoint(http.MethodPost, "/execute", handler.Handler)

	if err := cli.New().Run(); err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
}

//go run main.go run standalone