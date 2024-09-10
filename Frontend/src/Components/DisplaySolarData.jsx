import styled from "styled-components";

export default function DisplaySolarData({dataToDisplay, searchDateToDisplay, cityToDisplay, typeOfSunData, setIsDisplayed}){
    const date = new Date (dataToDisplay);
    const searchDate = new Date(searchDateToDisplay);
  
    const DivForData = styled.div`
    background-color: antiquewhite;
    border-radius: 10px;
    width: 300px;
    padding: 10px;`
    
    
    
    return(
        <>
            <DivForData>
                <h3>The requested data:</h3>
                <h4>City: {cityToDisplay}</h4>
                <h4>Date: {searchDate.toLocaleDateString()}</h4>
                <h4>{typeOfSunData}: {date.toLocaleDateString()} {date.toLocaleTimeString()}</h4>
                <h4>Timezone of displayed data: Europe/Budapest</h4>
            <button onClick={()=>setIsDisplayed(false)}>Go back</button>
            </DivForData>
        </>
    )
}