import axios from 'axios'

export async function fetchAsteroids(params) {
  const queryParams = new URLSearchParams()
  for (const key in params) {
    if (params[key] !== null && params[key] !== '') {
      queryParams.append(key, params[key])
    }
  }

  const response = await axios.get(`Asteroids?${queryParams}`).catch((err) => {
    if (err.status == 400) {
      throw new Error(err.response.data.detail)
    } else {
      throw new Error('Failed to fetch asteroids')
    }
  })

  return response.data
}

export async function fetchYears() {
  const response = await axios.get('Asteroids/years').catch(() => {
    throw new Error('Failed to fetch years')
  })
  return response.data.years
}

export async function fetchClasses() {
  const response = await axios.get('Asteroids/recclasses').catch(() => {
    throw new Error('Failed to fetch classes')
  })
  return response.data.recClasses
}
