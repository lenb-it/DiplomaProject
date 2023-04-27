import "./SectionHeader.css"

function SectionHeader({ title, imagePath }) {
	const sectionImg = {
		backgroundImage: `url(${imagePath})`,
		height: "300px"
	};

	return <>
		<div className="d-flex justify-content-center text-light align-items-center section-img"
			style={sectionImg} >
			<div className="fs-1 fw-bold">{title}</div>
		</div>
	</>
}


export default SectionHeader;