# Pomodoro Timer

Current version need install [Timer](https://github.com/caarlos0/timer)

# Command

```sh
# Turn on a 25-minute timer
pt work
# Turn on a 10-minute timer
pt break

# Turn on a 30-minute timer, this timer name is work
pt -d 30m -n work
```

| short name | long name  | description    |
|------------|------------|----------------|
| -d         | --duration | timer duration |
| -n         | --name     | timer name     |

## Config

Config file Path: `~/.config/pomodoro-timer/config.toml`

```toml
work_duration = "25m"
break_duration = "10m"
```

## Roadmap

- [ ] No dependency on timer command line program
- [ ] Implementation of a new end user interface

## LICENSE

This Project is released under the MIT