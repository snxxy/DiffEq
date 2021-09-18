import axios from 'axios'
import { reactive } from 'vue'

const equationCount = reactive({
    equationCountByType: [],
    equationTotal: 0
})

export function useGetEquationCount() {
    function getEquationCount() {
        axios.get('https://localhost:5001/api/homeapi/getequationcount')
            .then(response => {
                equationCount.equationCountByType = response.data
                getTotalEquations()
            })
    }

    function getTotalEquations() {
        equationCount.equationTotal = 0
        for (var i = 0, n = equationCount.equationCountByType.length; i < n; i++) {
            equationCount.equationTotal += equationCount.equationCountByType[i];
        }
    }
    return {
        equationCount,
        getEquationCount      
    }
}