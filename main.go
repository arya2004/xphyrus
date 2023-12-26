package main

import (
	"fmt"
	"os"

	"github.com/runabol/tork/cli"
	"github.com/runabol/tork/conf"
)

func main() {
   // Load the Tork config file (if exists) 
   if err := conf.LoadConfig(); err != nil {
     fmt.Println(err)
     os.Exit(1)
   }

   // Start the Tork CLI
   app := cli.New()
   if err := app.Run(); err != nil {
     fmt.Println(err)
     os.Exit(1)
   }
}